using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class RewardSpriteMapping
{
    public string value;
    public Sprite sprite;
}

[System.Serializable]
public class RewardResponse
{
    [JsonProperty("value")]
    public string value;
}
public class BlindbagManager : MonoBehaviour
{
    public RewardSpriteMapping[] rewardSpriteMappings; // Mapping từ key sang sprite
    private Dictionary<string, Sprite> rewardSpriteDict;

    public Text chancesText;
    public Image rewardImageDisplay;

    public GameObject out_of_chance_panel;
    public GameObject prize_popup_panel;
    public GameObject error_panel;

    public Transform historyContent;
    public GameObject historyItemPrefab;

    public AudioSource audioSource;
    public AudioClip openBlindBagMusic;
    public AudioClip rewardMusic;

    public GameObject baganim_panel;
    public GameObject bagBeforeTear;
    public GameObject bagAfterTear;
    public GameObject tearParticlePrefab;
    public RewardFetcher rewardFetcher;

    public GameObject customVoucherPrefab;
    public Transform prizePopupContent;

    public bool isRewardInProgress = false;
    public int totalTurn;
    void Start()
    {
        //totalTurn = APIManager.totalTurn;
        rewardSpriteDict = new Dictionary<string, Sprite>();
        foreach (var mapping in rewardSpriteMappings)
        {
            rewardSpriteDict[mapping.value] = mapping.sprite;
        }
    }
    public void UseChance()
    {
        if (isRewardInProgress)
        {
            return; // Nếu có quà đang được mở, không làm gì thêm
        }

        if (APIManager.totalTurn > 0)
        {
            chancesText.text = APIManager.totalTurn.ToString();
            isRewardInProgress = true;

            // Xử lý phần thưởng
            StartCoroutine(HandleRewardWithDelay());
        }
        else
        {
            out_of_chance_panel.SetActive(true);
        }
    }
    private IEnumerator HandleRewardWithDelay()
    {
        string authToken = APIManager.authToken;
        int spinTypeNumber = APIManager.spinTypeNumber;
        isRewardInProgress = true;
        bagBeforeTear.SetActive(true);

        // Hiệu ứng xé túi mù
        Animator animator = bagBeforeTear.GetComponent<Animator>();
        if (animator != null)
        {
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
            Debug.LogWarning("No Animator attached to bagBeforeTear!");
            yield return new WaitForSeconds(1f);
        }

        if (tearParticlePrefab != null)
        {
            GameObject particleInstance = Instantiate(tearParticlePrefab, bagBeforeTear.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
            Destroy(particleInstance, 2f);
        }

        bagBeforeTear.SetActive(false);
        bagAfterTear.SetActive(true);
        PlayOpenBlindBagMusic();

        if (!string.IsNullOrEmpty(authToken))
        {
            yield return StartCoroutine(SpinReward(authToken));
        }
        else
        {
            Debug.LogError("AuthToken is missing.");
        }
        bagAfterTear.SetActive(false);
        isRewardInProgress = false;

        yield return StartCoroutine(rewardFetcher.FetchRewards(authToken));
    }
    private void PlayOpenBlindBagMusic()
    {
        if (audioSource != null && openBlindBagMusic != null)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(openBlindBagMusic);
        }
    }

    private void PlayRewardMusic()
    {
        if (audioSource != null && rewardMusic != null)
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(rewardMusic);
        }
    }
    public IEnumerator SpinReward(string authToken)
    {
        string spinRewardUrl = "https://apigamifi.mobifoneplus.vn/rewards/spinReward";

        string jsonData = JsonConvert.SerializeObject(new
        {
            spinTypeNumber = APIManager.spinTypeNumber,
            tokenSso = APIManager.tokenSso,
            ctkmId = APIManager.ctkmId
        });
        UnityWebRequest request = new UnityWebRequest(spinRewardUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.SetRequestHeader("accept", "*");
        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
        request.SetRequestHeader("Content-Type", "application/json");
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;

            if (responseJson.Contains("\"exception\"") || responseJson.Contains("\"status\": 400"))
            {
                Debug.LogError("API returned an error response: " + responseJson);
                error_panel.SetActive(true);
                yield break; // Exit the coroutine
            }

            try
            {
                SpinResponse response = JsonConvert.DeserializeObject<SpinResponse>(responseJson);

                // Kiểm tra nếu response và data không phải là null
                if (response != null && response.Data != null)
                {
                    APIManager.totalTurn--;
                    chancesText.text = APIManager.totalTurn.ToString();
                    var rewardData = response.Data;
                    HashSet<int> validVoucherIds = new HashSet<int>
                    {
                        21105, 21102, 21099, 21096, 21093, 21090,
                        21087, 21084, 21081, 21078, 21075, 21069,
                        21066, 21129, 21111, 50433, 21117, 21129
                    };
                    if (rewardData.AdditionalVoucherData != null &&
                        validVoucherIds.Contains(rewardData.AdditionalVoucherData.Id))
                    {
                        GameObject customReward = Instantiate(customVoucherPrefab, prizePopupContent);
                        Text rewardText = customReward.GetComponentInChildren<Text>();
                        if (rewardText != null)
                        {
                            rewardText.text = rewardData.AdditionalVoucherData.Name;
                        }
                        Transform closeButtonTransform = customReward.transform.Find("CloseBtn");
                        if (closeButtonTransform != null)
                        {
                            Button closeButton = closeButtonTransform.GetComponent<Button>();
                            if (closeButton != null)
                            {
                                closeButton.onClick.AddListener(() => Destroy(customReward));
                            }
                        }
                    }
                    else if (rewardSpriteDict.ContainsKey(rewardData.Value))
                    {
                        rewardImageDisplay.sprite = rewardSpriteDict[rewardData.Value];
                        PlayRewardMusic();
                        prize_popup_panel.SetActive(true);
                    }
                    PlayRewardMusic();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"JSON Parsing Error: {ex.Message}");
                error_panel.SetActive(true);
            }
        }
        else
        {
            Debug.LogError($"Failed to spin reward: {request.error}");
            error_panel.SetActive(true);
        }
    }
    public void ClosePrizePopup()
    {
        prize_popup_panel.SetActive(false);
    }
}

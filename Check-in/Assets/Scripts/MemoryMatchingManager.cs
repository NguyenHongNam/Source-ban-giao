using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryMatchingManager : MonoBehaviour
{
    [Header("Game Settings")]
    public GameObject cardPrefab; // Prefab của thẻ
    public Sprite[] cardSprites; // Các hình ảnh của thẻ
    public Transform gridParent; // Nơi chứa các thẻ

    [Header("Game State")]
    private List<GameObject> spawnedCards = new List<GameObject>(); // Danh sách thẻ đã sinh ra
    private GameObject firstSelectedCard, secondSelectedCard; // Hai thẻ đang được lật
    private bool canSelect = true; // Có thể chọn thẻ hay không

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        // Tạo các cặp thẻ
        List<Sprite> cardList = new List<Sprite>();
        for (int i = 0; i < cardSprites.Length; i++)
        {
            cardList.Add(cardSprites[i]);
            cardList.Add(cardSprites[i]); // Mỗi hình ảnh xuất hiện 2 lần
        }

        // Xáo trộn các thẻ
        Shuffle(cardList);

        // Tạo thẻ trong Grid
        foreach (var sprite in cardList)
        {
            GameObject card = Instantiate(cardPrefab, gridParent);
            card.GetComponent<CardData>().SetCard(sprite, OnCardSelected);
            spawnedCards.Add(card);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void OnCardSelected(GameObject selectedCard)
    {
        if (!canSelect || selectedCard == firstSelectedCard) return;

        if (firstSelectedCard == null)
        {
            firstSelectedCard = selectedCard;
        }
        else
        {
            secondSelectedCard = selectedCard;
            canSelect = false;

            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstSelectedCard.GetComponent<CardData>().CardSprite ==
            secondSelectedCard.GetComponent<CardData>().CardSprite)
        {
            // Nếu khớp, ẩn hai thẻ
            Destroy(firstSelectedCard);
            Destroy(secondSelectedCard);
        }
        else
        {
            // Nếu không khớp, lật lại thẻ
            firstSelectedCard.GetComponent<CardData>().FlipBack();
            secondSelectedCard.GetComponent<CardData>().FlipBack();
        }

        firstSelectedCard = null;
        secondSelectedCard = null;
        canSelect = true;
    }
}

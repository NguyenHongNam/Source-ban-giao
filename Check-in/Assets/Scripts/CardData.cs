using UnityEngine;
using UnityEngine.UI;

public class CardData : MonoBehaviour
{
    public Sprite CardSprite { get; private set; }
    public Image frontImage; // Mặt trước của thẻ
    public Image backImage; // Mặt sau của thẻ

    private bool isFlipped = false;
    private System.Action<GameObject> onCardSelected;

    public void SetCard(Sprite sprite, System.Action<GameObject> onSelectedCallback)
    {
        CardSprite = sprite;
        frontImage.sprite = sprite;
        onCardSelected = onSelectedCallback;
    }

    public void OnClick()
    {
        if (isFlipped) return;
        Debug.Log($"Thẻ bài {CardSprite.name} đã được nhấp!");

        Flip();
        onCardSelected?.Invoke(gameObject);
    }

    public void Flip()
    {
        isFlipped = true;
        backImage.gameObject.SetActive(false);
        frontImage.gameObject.SetActive(true);
    }

    public void FlipBack()
    {
        isFlipped = false;
        backImage.gameObject.SetActive(true);
        frontImage.gameObject.SetActive(false);
    }
}

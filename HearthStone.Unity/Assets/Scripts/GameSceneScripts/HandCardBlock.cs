using HearthStone.Library;
using HearthStone.Protocol;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandCardBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text manaCostText;
    private Text nameText;
    private Image rarityImage;
    private Text descriptionText;
    private Text leftNumberText;
    private Text rightNumberText;

    private void Awake()
    {
        manaCostText = transform.Find("ManaCostText").GetComponent<Text>();
        nameText = transform.Find("NameText").GetComponent<Text>();
        rarityImage = transform.Find("RarityColor").GetComponent<Image>();
        descriptionText = transform.Find("DescriptionText").GetComponent<Text>();
        leftNumberText = transform.Find("LeftNumber/Text").GetComponent<Text>();
        rightNumberText = transform.Find("RightNumber/Text").GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = 2 * Vector2.one;
        transform.localPosition += new Vector3(0, 50, 0);
        transform.SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector2.one;
        transform.localPosition -= new Vector3(0, 50, 0);
    }

    public void RenderCard(CardRecord record, int gamePlayerID)
    {
        manaCostText.text = "NotImpl.";
        nameText.text = record.Card.CardName;
        switch (record.Card.Rarity)
        {
            case RarityCode.Common:
                rarityImage.color = Color.white;
                break;
            case RarityCode.Rare:
                rarityImage.color = Color.blue;
                break;
            case RarityCode.Epic:
                rarityImage.color = new Color(155f / 255, 94f / 255, 246f / 255);
                break;
            case RarityCode.Legendary:
                rarityImage.color = new Color(255f / 255, 193f / 255, 58f / 255);
                break;
            default:
                rarityImage.color = Color.gray;
                break;
        }
        descriptionText.text = "NotImpl.";
        switch (record.Card.CardType)
        {
            case CardTypeCode.Servant:
                leftNumberText.text = "?";
                rightNumberText.text = "?";
                break;
            case CardTypeCode.Spell:
                leftNumberText.gameObject.SetActive(false);
                rightNumberText.gameObject.SetActive(false);
                break;
            case CardTypeCode.Weapon:
                leftNumberText.text = "?";
                rightNumberText.text = "?";
                break;
        }
    }
}

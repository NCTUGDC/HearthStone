using HearthStone.Library.Cards;
using HearthStone.Protocol;
using UnityEngine;
using UnityEngine.UI;
public class WeaponCardBlock : MonoBehaviour
{
    private Text manaCostText;
    private Text nameText;
    private Image rarityImage;
    private Text descriptionText;
    private Text attackText;
    private Text durabilityText;

    public WeaponCard Card { get; private set; }

    private void Awake()
    {
        manaCostText = transform.Find("ManaCostText").GetComponent<Text>();
        nameText = transform.Find("NameText").GetComponent<Text>();
        rarityImage = transform.Find("RarityColor").GetComponent<Image>();
        descriptionText = transform.Find("DescriptionText").GetComponent<Text>();
        attackText = transform.Find("Attack/Text").GetComponent<Text>();
        durabilityText = transform.Find("Durability/Text").GetComponent<Text>();
    }
    public void SetCard(WeaponCard card)
    {
        Card = card;
        manaCostText.text = card.ManaCost.ToString();
        nameText.text = card.CardName;
        switch (card.Rarity)
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
        descriptionText.text = card.Description;
        attackText.text = card.Attack.ToString();
        durabilityText.text = card.Durability.ToString();
    }
}

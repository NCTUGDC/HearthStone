using HearthStone.Library;
using UnityEngine;
using UnityEngine.UI;

public abstract class CardBlock : MonoBehaviour
{
    private Text manaCostText;
    private Text nameText;
    private Image rarityImage;
    private Text descriptionText;

    public Card Card { get; private set; }

    protected virtual void Awake()
    {
        manaCostText = transform.Find("ManaCostText").GetComponent<Text>();
        nameText = transform.Find("NameText").GetComponent<Text>();
        rarityImage = transform.Find("RarityColor").GetComponent<Image>();
        descriptionText = transform.Find("DescriptionText").GetComponent<Text>();
    }

    public virtual void SetCard(Card card)
    {
        Card = card;
        manaCostText.text = card.ManaCost.ToString();
        nameText.text = card.CardName;
        rarityImage.color = RarityColorSelector.RarityToColor(card.Rarity);
        descriptionText.text = card.Description(null, 0);
    }
}

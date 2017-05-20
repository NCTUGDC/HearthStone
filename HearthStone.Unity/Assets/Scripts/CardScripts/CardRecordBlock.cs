using HearthStone.Library;
using UnityEngine;
using UnityEngine.UI;

public class CardRecordBlock : MonoBehaviour
{
    protected Text manaCostText;
    protected Text nameText;
    protected Image rarityImage;
    protected Text descriptionText;

    public CardRecord Card { get; private set; }

    protected virtual void Awake()
    {
        manaCostText = transform.Find("ManaCostText").GetComponent<Text>();
        nameText = transform.Find("NameText").GetComponent<Text>();
        rarityImage = transform.Find("RarityColor").GetComponent<Image>();
        descriptionText = transform.Find("DescriptionText").GetComponent<Text>();
    }
    public virtual void SetCard(CardRecord card, int selfGamePlayerID)
    {
        Card = card;
        manaCostText.text = card.ManaCost.ToString();
        if (card.ManaCost < card.Card.ManaCost)
        {
            manaCostText.color = Color.green;
        }
        else if(card.ManaCost > card.Card.ManaCost)
        {
            manaCostText.color = Color.red;
        }
        nameText.text = card.Card.CardName;
        rarityImage.color = RarityColorSelector.RarityToColor(card.Card.Rarity);
        descriptionText.text = card.Card.Description(GameInstance.Game, selfGamePlayerID);
    }
}

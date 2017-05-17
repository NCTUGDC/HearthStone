using HearthStone.Library;
using HearthStone.Library.Cards;
using HearthStone.Library.CardRecords;
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
        rarityImage.color = RarityColorSelector.RarityToColor(record.Card.Rarity);
        descriptionText.text = record.Card.Description(GameInstance.Game, gamePlayerID);
        switch (record.Card.CardType)
        {
            case CardTypeCode.Servant:
                {
                    ServantCardRecord servantCard = record as ServantCardRecord;
                    leftNumberText.text = servantCard.Attack.ToString();
                    rightNumberText.text = servantCard.Health.ToString();

                    if(servantCard.Attack > (servantCard.Card as ServantCard).Attack)
                    {
                        leftNumberText.color = Color.green;
                    }

                    if(servantCard.RemainedHealth == servantCard.Health)
                    {
                        if(servantCard.Health > (servantCard.Card as ServantCard).Health)
                        {
                            rightNumberText.color = Color.green;
                        }
                    }
                    else
                    {
                        rightNumberText.color = Color.red;
                    }
                }
                break;
            case CardTypeCode.Spell:
                leftNumberText.transform.parent.gameObject.SetActive(false);
                rightNumberText.transform.parent.gameObject.SetActive(false);
                break;
            case CardTypeCode.Weapon:
                {
                    WeaponCardRecord weaponCard = record as WeaponCardRecord;
                    leftNumberText.text = weaponCard.Attack.ToString();
                    rightNumberText.text = weaponCard.Durability.ToString();

                    if (weaponCard.Attack > (weaponCard.Card as WeaponCard).Attack)
                    {
                        leftNumberText.color = Color.green;
                    }
                    if (weaponCard.Durability > (weaponCard.Card as WeaponCard).Durability)
                    {
                        rightNumberText.color = Color.green;
                    }
                }
                break;
        }
    }
}

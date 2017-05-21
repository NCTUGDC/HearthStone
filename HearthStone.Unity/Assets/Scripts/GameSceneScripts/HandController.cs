using HearthStone.Library;
using HearthStone.Protocol;
using System.Linq;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private CardRecordBlock handServantCardRecordBlockPrefab;
    [SerializeField]
    private CardRecordBlock handSpellCardRecordBlockPrefab;
    [SerializeField]
    private CardRecordBlock handWeaponCardRecordBlockPrefab;
    [SerializeField]
    private GameObject emptyHandCardBlockPrefab;

    public void RenderHand(GamePlayer gamePlayer, bool isOpponent)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        int handCardCount = gamePlayer.HandCardIDs.Count();
        int index = 0;
        foreach (var cardRecordID in gamePlayer.HandCardIDs)
        {
            CardRecord card;
            if (isOpponent)
            {
                GameObject handCard = Instantiate(emptyHandCardBlockPrefab, transform);
                handCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95 * handCardCount / 2 + index * 95, 10);
            }
            else if(GameInstance.Game.GameCardManager.FindCard(cardRecordID, out card))
            {
                CardRecordBlock handCard = null;
                switch(card.Card.CardType)
                {
                    case CardTypeCode.Servant:
                        handCard = Instantiate(handServantCardRecordBlockPrefab, transform);
                        break;
                    case CardTypeCode.Spell:
                        handCard = Instantiate(handSpellCardRecordBlockPrefab, transform);
                        break;
                    case CardTypeCode.Weapon:
                        handCard = Instantiate(handWeaponCardRecordBlockPrefab, transform);
                        break;
                }
                handCard.SetCard(card, gamePlayer.GamePlayerID);
                handCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-95 * handCardCount / 2 + index * 95, 10);
            }
            index++;
        }
    }
}

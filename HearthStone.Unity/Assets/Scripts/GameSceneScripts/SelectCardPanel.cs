using HearthStone.Library;
using HearthStone.Protocol;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectCardPanel : MonoBehaviour
{
    [SerializeField]
    private CardRecordBlock servantCardRecordBlockPrefab;
    [SerializeField]
    private CardRecordBlock spellCardRecordBlockPrefab;
    [SerializeField]
    private CardRecordBlock weaponCardRecordBlockPrefab;
    [SerializeField]
    private GameObject cardBackBlockPrefab;

    public int[] SelectedCardRecordIDs
    {
        get
        {
            List<int> cardRecordIDs = new List<int>();
            foreach(Transform child in transform)
            {
                if(child.GetComponent<Toggle>().isOn)
                    cardRecordIDs.Add(child.GetComponent<CardRecordBlock>().Card.CardRecordID);
            }
            return cardRecordIDs.ToArray();
        }
    }

    public void RenderCards(IEnumerable<int> cardIDs, bool isOpponent)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        int cardCount = cardIDs.Count();
        int index = 0;
        foreach (var cardID in cardIDs)
        {
            CardRecord card;
            if (isOpponent)
            {
                GameObject handCard = Instantiate(cardBackBlockPrefab, transform);
                handCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-130 * cardCount / 2 + index * 130, 0);
            }
            else if(GameInstance.Game.GameCardManager.FindCard(cardID, out card))
            {
                CardRecordBlock handCard = null;
                switch (card.Card.CardType)
                {
                    case CardTypeCode.Servant:
                        handCard = Instantiate(servantCardRecordBlockPrefab, transform);
                        break;
                    case CardTypeCode.Spell:
                        handCard = Instantiate(spellCardRecordBlockPrefab, transform);
                        break;
                    case CardTypeCode.Weapon:
                        handCard = Instantiate(weaponCardRecordBlockPrefab, transform);
                        break;
                }
                handCard.SetCard(card, GameInstance.SelfGamePlayer.GamePlayerID);
                handCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-130 * cardCount / 2 + index * 130, 0);
            }
            index++;
        }
    }
}

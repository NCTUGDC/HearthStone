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

    public IEnumerable<int> SelectedCardRecordIDs
    {
        get
        {
            ToggleGroup group = GetComponent<ToggleGroup>();
            return group.ActiveToggles().Select(x => x.GetComponent<CardRecordBlock>().Card.CardRecordID);
        }
    }

    public void RenderCards(IEnumerable<CardRecord> cards, bool isOpponent)
    {
        foreach (GameObject child in transform)
        {
            Destroy(child);
        }
        int cardCount = cards.Count();
        int index = 0;
        foreach (var card in cards)
        {
            if (isOpponent)
            {
                GameObject handCard = Instantiate(cardBackBlockPrefab, transform);
                handCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(-130 * cardCount / 2 + index * 130, 0);
            }
            else
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
                handCard.GetComponent<Toggle>().group = GetComponent<ToggleGroup>();
            }
            index++;
        }
    }
}

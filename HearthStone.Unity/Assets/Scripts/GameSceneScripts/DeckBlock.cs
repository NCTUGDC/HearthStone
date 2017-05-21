using HearthStone.Library;
using HearthStone.Protocol;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DeckBlock : MonoBehaviour
{
    private GamePlayer gamePlayer;
    private Text deckCardCountText;

    private void Awake()
    {
        deckCardCountText = GetComponentInChildren<Text>();
    }
    private void OnDestroy()
    {
        gamePlayer.Deck.OnCardsChanged -= UpdateDeckCardCountText;
    }

    public void ObserveGamePlayer(GamePlayer gamePlayer)
    {
        this.gamePlayer = gamePlayer;
        gamePlayer.Deck.OnCardsChanged += UpdateDeckCardCountText;
        UpdateDeckCardCountText(gamePlayer.Deck, 0, DataChangeCode.Update);
    }
    private void UpdateDeckCardCountText(GameDeck deck, int cardRecordID, DataChangeCode changeCode)
    {
        deckCardCountText.text = string.Format("{0}", deck.CardRecordIDs.Count());
    }
}

using HearthStone.Library;
using HearthStone.Protocol;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayerController : MonoBehaviour
{
    private ManaCrystalBlock manaCrystalBlock;
    private DeckBlock deckBlock;
    private Text nicknameText;
    private HandController hand;
    private bool isOpponent;

    private void Awake()
    {
        manaCrystalBlock = transform.Find("ManaCrystalBlock").GetComponent<ManaCrystalBlock>();
        deckBlock = transform.Find("DeckBlock").GetComponent<DeckBlock>();
        nicknameText = transform.Find("NicknameText").GetComponent<Text>();
        hand = transform.Find("Hand").GetComponent<HandController>();
    }

    public void InitialGamePlayer(GamePlayer gamePlayer, bool isOpponent)
    {
        this.isOpponent = isOpponent;
        manaCrystalBlock.ObserveGamePlayer(gamePlayer);
        deckBlock.ObserveGamePlayer(gamePlayer);
        nicknameText.text = gamePlayer.Player.Nickname;
        hand.RenderHand(gamePlayer, isOpponent);

        gamePlayer.OnHandCardsChanged += RenderHands;
    }
    private void RenderHands(GamePlayer gamePlayer, int cardRecordID, DataChangeCode changeCode)
    {
        hand.RenderHand(gamePlayer, isOpponent);
    }
}

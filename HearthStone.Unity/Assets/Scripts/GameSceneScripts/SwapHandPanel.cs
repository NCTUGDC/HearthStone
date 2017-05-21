using HearthStone.Library;
using HearthStone.Protocol;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SwapHandPanel : MonoBehaviour
{
    [SerializeField]
    private SelectCardPanel opponentSelectPanel;
    [SerializeField]
    private SelectCardPanel selfSelectPanel;
    [SerializeField]
    private Button confirmButton;

    private void Start()
    {
        selfSelectPanel.RenderCards(GameInstance.SelfGamePlayer.HandCardIDs, false);
        opponentSelectPanel.RenderCards(GameInstance.OpponentGamePlayer.HandCardIDs, true);
        GameInstance.SelfGamePlayer.OnHandCardsChanged += OnSelfHandCardsChanged;
        GameInstance.OpponentGamePlayer.OnHandCardsChanged += OnOpponentHandCardsChanged;
        GameInstance.SelfGamePlayer.OnHasChangedHandChanged += OnSelfHandChanged;
        GameInstance.Game.OnRoundCountChanged += OnGameStart;
    }
    private void OnOpponentHandCardsChanged(GamePlayer gamePlayer, int cardRecordID, DataChangeCode changeCode)
    {
        opponentSelectPanel.RenderCards(GameInstance.OpponentGamePlayer.HandCardIDs, true);
    }
    private void OnSelfHandCardsChanged(GamePlayer gamePlayer, int cardRecordID, DataChangeCode changeCode)
    {
        selfSelectPanel.RenderCards(GameInstance.SelfGamePlayer.HandCardIDs, false);
    }
    private void OnSelfHandChanged(GamePlayer gamePlayer)
    {
        confirmButton.gameObject.SetActive(false);
    }
    private void OnGameStart(Game game)
    {
        if(game.RoundCount == 1)
        {
            GameInstance.SelfGamePlayer.OnHandCardsChanged -= OnSelfHandCardsChanged;
            GameInstance.OpponentGamePlayer.OnHandCardsChanged -= OnOpponentHandCardsChanged;
            GameInstance.SelfGamePlayer.OnHasChangedHandChanged -= OnSelfHandChanged;
            GameInstance.Game.OnRoundCountChanged -= OnGameStart;
            gameObject.SetActive(false);
        }
    }

    public void SwapCards()
    {
        PlayerManager.Player.OperationManager.SwapHands(GameInstance.Game.GameID, selfSelectPanel.SelectedCardRecordIDs);
        confirmButton.interactable = false;
    }
}

using HearthStone.Library;
using HearthStone.Protocol;
using UnityEngine;
using UnityEngine.UI;

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
        selfSelectPanel.RenderCards(GameInstance.SelfGamePlayer.HandCards, false);
        opponentSelectPanel.RenderCards(GameInstance.OpponentGamePlayer.HandCards, true);
        GameInstance.SelfGamePlayer.OnHandCardsChanged += OnSelfHandCardsChanged;
        GameInstance.OpponentGamePlayer.OnHandCardsChanged += OnOpponentHandCardsChanged;
        GameInstance.SelfGamePlayer.OnHasChangedHandChanged += OnSelfHandChanged;
        GameInstance.Game.OnRoundCountChanged += OnGameStart;
    }
    private void OnOpponentHandCardsChanged(CardRecord card, DataChangeCode changeCode)
    {
        opponentSelectPanel.RenderCards(GameInstance.OpponentGamePlayer.HandCards, true);
    }
    private void OnSelfHandCardsChanged(CardRecord card, DataChangeCode changeCode)
    {
        selfSelectPanel.RenderCards(GameInstance.SelfGamePlayer.HandCards, false);
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

    }
}

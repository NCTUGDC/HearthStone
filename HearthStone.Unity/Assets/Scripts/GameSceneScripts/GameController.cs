using HearthStone.Library;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GamePlayerController self;
    [SerializeField]
    private GamePlayerController opponent;
    [SerializeField]
    private SwapHandPanel swapHandPanel;
    [SerializeField]
    private Button endTurnButton;


    void Start ()
    {
        Game game = GameInstance.Game;
        if(game.GamePlayer1.Player.PlayerID == EndPointManager.EndPoint.Player.PlayerID)
        {
            self.InitialGamePlayer(game.GamePlayer1, false);
            opponent.InitialGamePlayer(game.GamePlayer2, true);
        }
        else
        {
            self.InitialGamePlayer(game.GamePlayer2, false);
            opponent.InitialGamePlayer(game.GamePlayer1, true);
        }
        game.OnCurrentGamePlayerID_Changed += SwithOnEndTurnButton;

        endTurnButton.interactable = GameInstance.Game.CurrentGamePlayerID == GameInstance.SelfGamePlayer.GamePlayerID;
    }

    public void EndTurn()
    {
        if(GameInstance.Game.CurrentGamePlayerID == GameInstance.SelfGamePlayer.GamePlayerID)
        {
            endTurnButton.interactable = false;
            PlayerManager.Player.OperationManager.EndTurn(GameInstance.Game.GameID);
        }
    }
    private void SwithOnEndTurnButton(Game game)
    {
        if(game.CurrentGamePlayerID == GameInstance.SelfGamePlayer.GamePlayerID)
        {
            endTurnButton.interactable = true;
        }
    }
}

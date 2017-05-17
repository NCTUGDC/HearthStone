using HearthStone.Library;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GamePlayerController self;
    [SerializeField]
    private GamePlayerController opponent;

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
    }
}

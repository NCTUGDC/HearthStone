using HearthStone.Library;

public static class GameInstance
{
    public static Game Game { get; set; }
    public static GamePlayer SelfGamePlayer
    {
        get
        {
            if (Game.GamePlayer1.Player.PlayerID == EndPointManager.EndPoint.Player.PlayerID)
                return Game.GamePlayer1;
            else
                return Game.GamePlayer2;
        }
    }
    public static GamePlayer OpponentGamePlayer
    {
        get
        {
            if (Game.GamePlayer1.Player.PlayerID == EndPointManager.EndPoint.Player.PlayerID)
                return Game.GamePlayer2;
            else
                return Game.GamePlayer1;
        }
    }
}

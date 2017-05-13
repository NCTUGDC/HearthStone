using HearthStone.Library;
using System;

namespace HearthStone.Server
{
    public class GameFactory : GameManager
    {
        private static int gameCounter = 0;

        public void CreateGame(Tuple<Player, Deck> playerDeckPair1, Tuple<Player, Deck> playerDeckPair2)
        {
            Game game = new Game(gameCounter++, playerDeckPair1.Item1, playerDeckPair2.Item1, playerDeckPair1.Item2, playerDeckPair2.Item2);
            AddGame(game);
            game.GamePlayer1.Player.EventManager.GameStart(game);
            game.GamePlayer2.Player.EventManager.GameStart(game);
        }
    }
}

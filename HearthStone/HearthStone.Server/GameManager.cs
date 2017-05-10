using HearthStone.Library;
using System;
using System.Collections.Generic;

namespace HearthStone.Server
{
    public class GameManager
    {
        public static GameManager Instance { get; private set; }
        private static int gameCounter = 0;
        static GameManager()
        {
            Instance = new GameManager();
        }

        private Dictionary<int, Game> gameDictionary = new Dictionary<int, Game>();

        private GameManager()
        {

        }
        public bool FindGame(int gameID, out Game game)
        {
            if(gameDictionary.ContainsKey(gameID))
            {
                game = gameDictionary[gameID];
                return true;
            }
            else
            {
                game = null;
                return false;
            }
        }
        public bool RemoveGame(int gameID)
        {
            if(gameDictionary.ContainsKey(gameID))
            {
                return gameDictionary.Remove(gameID);
            }
            else
            {
                return false;
            }
        }
        public void CreateGame(Tuple<Player, Deck> playerDeckPair1, Tuple<Player, Deck> playerDeckPair2)
        {
            Game game = new Game(gameCounter++, playerDeckPair1.Item1, playerDeckPair2.Item1, playerDeckPair1.Item2, playerDeckPair2.Item2);
            gameDictionary.Add(game.GameID, game);
        }
    }
}

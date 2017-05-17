using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public class GameManager
    {
        public static GameManager Instance { get; private set; }

        public static void Initial(GameManager gameManager)
        {
            Instance = gameManager;
        }

        private Dictionary<int, Game> gameDictionary = new Dictionary<int, Game>();

        public delegate void GameChangeEventHandler(Game game, DataChangeCode changeCode);
        private event GameChangeEventHandler onGameChanged;
        public event GameChangeEventHandler OnGameChanged { add { onGameChanged += value; } remove { onGameChanged -= value; } }

        public GameManager()
        {

        }
        public bool FindGame(int gameID, out Game game)
        {
            if (gameDictionary.ContainsKey(gameID))
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
        public bool AddGame(Game game)
        {
            if (!gameDictionary.ContainsKey(game.GameID))
            {
                gameDictionary.Add(game.GameID, game);
                onGameChanged?.Invoke(game, DataChangeCode.Add);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveGame(int gameID)
        {
            if (gameDictionary.ContainsKey(gameID))
            {
                Game game = gameDictionary[gameID];
                onGameChanged?.Invoke(game, DataChangeCode.Remove);
                return gameDictionary.Remove(gameID);
            }
            else
            {
                return false;
            }
        }
    }
}

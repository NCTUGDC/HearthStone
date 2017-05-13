using HearthStone.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HearthStone.Server
{
    public class PlayerMatchManager
    {
        public static PlayerMatchManager Instance { get; private set; }
        public static void Initial()
        {
            Instance = new PlayerMatchManager();
        }

        Dictionary<int, Tuple<Player, Deck>> waitingPlayerDictionary = new Dictionary<int, Tuple<Player, Deck>>();
        public int WaitingPlayerCount
        {
            get
            {
                return waitingPlayerDictionary.Count;
            }
        }

        private event Action<int> onWaitingPlayerCountUpdated;
        public event Action<int> OnWaitingPlayerCountUpdated { add { onWaitingPlayerCountUpdated += value; } remove { onWaitingPlayerCountUpdated -= value; } }

        private PlayerMatchManager()
        {
            Task.Run(() => 
            {
                while(true)
                {
                    Tuple<Player, Deck> playerDeckPair1, playerDeckPair2;
                    if(MatchTwoPlayer(out playerDeckPair1, out playerDeckPair2))
                    {
                        (GameManager.Instance as GameFactory).CreateGame(playerDeckPair1, playerDeckPair2);
                    }
                    Thread.Sleep(100);
                }
            });

            OnWaitingPlayerCountUpdated += (value) => 
            {
                WaitingPlayerCounter.WaitingPlayerCount = value;
            };
        }

        public void AddPlayer(Player player, Deck deck)
        {
            if (!waitingPlayerDictionary.ContainsKey(player.PlayerID))
            {
                waitingPlayerDictionary.Add(player.PlayerID, new Tuple<Player, Deck>(player, deck));
                onWaitingPlayerCountUpdated?.Invoke(WaitingPlayerCount);
                player.EndPoint.OnPlayerOffline += RemoveOfflinedPlayer;
            }
        }
        public bool MatchTwoPlayer(out Tuple<Player, Deck> playerDeckPair1, out Tuple<Player, Deck> playerDeckPair2)
        {
            if (WaitingPlayerCount >= 2)
            {
                Tuple<Player, Deck>[] players = waitingPlayerDictionary.Take(2).Select(x => x.Value).ToArray();
                playerDeckPair1 = players[0];
                playerDeckPair2 = players[1];
                waitingPlayerDictionary.Remove(playerDeckPair1.Item1.PlayerID);
                waitingPlayerDictionary.Remove(playerDeckPair2.Item1.PlayerID);
                onWaitingPlayerCountUpdated?.Invoke(WaitingPlayerCount);
                return true;
            }
            else
            {
                playerDeckPair1 = null;
                playerDeckPair2 = null;
                return false;
            }
        }
        private void RemoveOfflinedPlayer(Player player)
        {
            RemovePlayer(player.PlayerID);
            player.EndPoint.OnPlayerOffline -= RemoveOfflinedPlayer;
        }
        public void RemovePlayer(int playerID)
        {
            if (waitingPlayerDictionary.ContainsKey(playerID))
            {
                waitingPlayerDictionary.Remove(playerID);
                onWaitingPlayerCountUpdated?.Invoke(WaitingPlayerCount);
            }
        }
    }
}

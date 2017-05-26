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
            AssemblyGame(game);
            AddGame(game);
            LogService.Info("Game Instantiate");
            try
            {
                AssemblyGamePlayer(game.GamePlayer1);
                AssemblyGamePlayer(game.GamePlayer2);
                game.GamePlayer1.Player.EventManager.GameStart(game);
                game.GamePlayer2.Player.EventManager.GameStart(game);
            }
            catch(Exception ex)
            {
                LogService.Fatal(ex.Message);
                LogService.Fatal(ex.StackTrace);
            }
            LogService.Info("Game Start");
        }
        private void AssemblyGame(Game game)
        {
            //game.OnRoundStart += game.EventManager.RoundStart;
            //game.OnRoundEnd += game.EventManager.RoundEnd;
            game.OnGameOver += game.EventManager.GameOver;
            game.OnRoundCountChanged += game.EventManager.SyncDataBroker.SyncRoundCountChanged;
            game.OnCurrentGamePlayerID_Changed += game.EventManager.SyncDataBroker.SyncCurrentGamePlayerID_Changed;
        }
        private void AssemblyGamePlayer(GamePlayer gamePlayer)
        {
            gamePlayer.OnHasChangedHandChanged += gamePlayer.EventManager.SyncDataBroker.SyncHasChangedHandChanged;
            gamePlayer.OnHandCardsChanged += gamePlayer.EventManager.SyncDataBroker.SyncHandCardsChanged;
            gamePlayer.OnRemainedManaCrystalChanged += gamePlayer.EventManager.SyncDataBroker.SyncRemainedManaCrystalChanged;
            gamePlayer.OnManaCrystalChanged += gamePlayer.EventManager.SyncDataBroker.SyncManaCrystalChanged;

            //gamePlayer.Deck.OnDrawCard += gamePlayer.EventManager.DrawCard;
            gamePlayer.Deck.OnCardsChanged += gamePlayer.EventManager.SyncDataBroker.SyncDeckCardsChanged;
        }
    }
}

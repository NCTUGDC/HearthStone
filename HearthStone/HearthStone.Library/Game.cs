using HearthStone.Library.CommunicationInfrastructure.Event.Managers;
using System;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public class Game
    {
        public int GameID { get; private set; }
        public GamePlayer GamePlayer1 { get; private set; }
        public GamePlayer GamePlayer2 { get; private set; }

        private int roundCount;
        public int RoundCount
        {
            get { return roundCount; }
            private set
            {
                roundCount = value;
                OnRoundCountChanged?.Invoke(this);
            }
        }

        private int cardRecordID_Generator = 1;
        public int NewCardRecordID { get { return cardRecordID_Generator++; } }

        public Field Field1 { get; private set; }
        public Field Field2 { get; private set; }

        public int CurrentGamePlayerID { get; private set; }

        public event Action<Game> OnRoundCountChanged;
        public event Action<Game> OnRoundStart;
        public event Action<Game> OnRoundEnd;

        public GameEventManager EventManager { get; private set; }

        public Game(int gameID, Player player1, Player player2, Deck player1Deck, Deck player2Deck)
        {
            GameID = gameID;
            GamePlayer1 = new GamePlayer(player1, new Hero(1, 30, 30), CreateGameDeck(1, player1Deck));
            GamePlayer2 = new GamePlayer(player2, new Hero(2, 30, 30), CreateGameDeck(2, player2Deck));
            RoundCount = 0;
            Random randomGenerator = new Random();
            if (randomGenerator.NextDouble() > 0.5)
            {
                CurrentGamePlayerID = 1;
                GamePlayer1.Draw(3);
                GamePlayer2.Draw(4);
            }
            else
            {
                CurrentGamePlayerID = 2;
                GamePlayer1.Draw(4);
                GamePlayer2.Draw(3);
            }
            Field1 = new Field();
            Field2 = new Field();

            GamePlayer1.OnHasChangedHandChanged += DetectGamePlayerChangeHand;
            GamePlayer2.OnHasChangedHandChanged += DetectGamePlayerChangeHand;

            EventManager = new GameEventManager(this);
        }
        public Game(int gameID, GamePlayer gamePlayer1, GamePlayer gamePlayer2, int roundCount, int currentGamePlayerID)
        {
            GameID = gameID;
            GamePlayer1 = gamePlayer1;
            GamePlayer2 = gamePlayer2;
            RoundCount = roundCount;
            CurrentGamePlayerID = currentGamePlayerID;
            Field1 = new Field();
            Field2 = new Field();

            EventManager = new GameEventManager(this);
        }
        public GameDeck CreateGameDeck(int gameDeckID, Deck deck)
        {
            List<CardRecord> cardRecords = new List<CardRecord>();
            foreach (Card card in deck.Cards)
            {
                cardRecords.Add(card.CreateRecord(NewCardRecordID));
            }
            GameDeck gameDeck = new GameDeck(gameDeckID, cardRecords);
            gameDeck.Shuffle(100);
            return gameDeck;
        }
        private void DetectGamePlayerChangeHand(GamePlayer gamePlayer)
        {
            if(GamePlayer1.HasChangedHand && GamePlayer2.HasChangedHand)
            {
                RoundCount = 1;
            }
        }

        public void EndRound()
        {
            OnRoundEnd?.Invoke(this);
            if(CurrentGamePlayerID == 1)
            {
                CurrentGamePlayerID = 2;
            }
            else
            {
                CurrentGamePlayerID = 1;
            }
            RoundStart();
        }
        private void RoundStart()
        {
            GamePlayer player = (CurrentGamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            player.ManaCrystal++;
            player.RemainedManaCrystal = player.ManaCrystal;
            OnRoundStart?.Invoke(this);
            player.Draw(1);
        }
        public int SelectGamePlayerID(int playerID)
        {
            if(GamePlayer1.Player.PlayerID == playerID)
            {
                return 1;
            }
            else if(GamePlayer2.Player.PlayerID == playerID)
            {
                return 2;
            }
            else
            {
                return -1;
            }
        }
    }
}

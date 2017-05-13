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
                onRoundCountChanged?.Invoke(this);
            }
        }

        private int cardRecordID_Generator = 1;
        public int NewCardRecordID { get { return cardRecordID_Generator++; } }

        private List<CardRecord> field1Cards = new List<CardRecord>();
        private List<CardRecord> field2Cards = new List<CardRecord>();
        public IEnumerable<CardRecord> Field1Cards { get { throw new NotImplementedException(); } }
        public IEnumerable<CardRecord> Field2Cards { get { throw new NotImplementedException(); } }

        public int CurrentGamePlayerID { get; private set; }

        private event Action<Game> onRoundCountChanged;
        public event Action<Game> OnRoundCountChanged { add { onRoundCountChanged += value; } remove { onRoundCountChanged -= value; } }

        private event Action<Game> onRoundStart;
        public event Action<Game> OnRoundStart { add { onRoundStart += value; } remove { onRoundStart -= value; } }
        private event Action<Game> onRoundEnd;
        public event Action<Game> OnRoundEnd { add { onRoundEnd += value; } remove { onRoundEnd -= value; } }

        public GameEventManager EventManager { get; private set; }

        public Game(int gameID, Player player1, Player player2, Deck player1Deck, Deck player2Deck)
        {
            GameID = gameID;
            GamePlayer1 = new GamePlayer(player1, new Hero(1, 30, 30, false), CreateGameDeck(1, player1Deck));
            GamePlayer2 = new GamePlayer(player2, new Hero(2, 30, 30, false), CreateGameDeck(2, player2Deck));
            RoundCount = 0;
            Random randomGenerator = new Random();
            if(randomGenerator.NextDouble() > 0.5)
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
            onRoundEnd?.Invoke(this);
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
            onRoundStart?.Invoke(this);
            player.Draw(1);
        }
    }
}

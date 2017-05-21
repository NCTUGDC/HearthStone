using HearthStone.Library.CardRecords;
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
            set
            {
                roundCount = value;
                OnRoundCountChanged?.Invoke(this);
            }
        }

        public Field Field1 { get; private set; }
        public Field Field2 { get; private set; }

        private int currentGamePlayerID;
        public int CurrentGamePlayerID
        {
            get { return currentGamePlayerID; }
            set
            {
                currentGamePlayerID = value;
                OnCurrentGamePlayerID_Changed?.Invoke(this);
            }
        }

        public GameCardManager GameCardManager { get; private set; }

        public event Action<Game> OnRoundCountChanged;
        public event Action<Game> OnRoundStart;
        public event Action<Game> OnRoundEnd;
        public event Action<Game> OnCurrentGamePlayerID_Changed;
        public event Action<Game, int> OnGameOver;

        public GameEventManager EventManager { get; private set; }

        public Game(int gameID, Player player1, Player player2, Deck player1Deck, Deck player2Deck)
        {
            GameCardManager = new GameCardManager();
            GameCardManager.BindGame(this);

            GameID = gameID;
            GamePlayer1 = new GamePlayer(player1, new Hero(1, 30, 30), CreateGameDeck(1, player1Deck));
            GamePlayer1.BindGame(this);
            GamePlayer2 = new GamePlayer(player2, new Hero(2, 30, 30), CreateGameDeck(2, player2Deck));
            GamePlayer2.BindGame(this);
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
            Field1 = new Field(1);
            Field1.BindGame(this);
            Field2 = new Field(2);
            Field2.BindGame(this);

            GamePlayer1.OnHasChangedHandChanged += DetectGamePlayerChangeHand;
            GamePlayer2.OnHasChangedHandChanged += DetectGamePlayerChangeHand;

            EventManager = new GameEventManager(this);
        }

        private void Hero_OnRemainedHPChanged(Hero hero, int delta)
        {
            throw new NotImplementedException();
        }

        public Game(int gameID, GamePlayer gamePlayer1, GamePlayer gamePlayer2, int roundCount, int currentGamePlayerID, GameCardManager gameCardManager)
        {
            GameID = gameID;
            GamePlayer1 = gamePlayer1;
            GamePlayer2 = gamePlayer2;
            RoundCount = roundCount;
            CurrentGamePlayerID = currentGamePlayerID;
            Field1 = new Field(1);
            Field1.BindGame(this);
            Field2 = new Field(2);
            Field2.BindGame(this);
            GameCardManager = gameCardManager;
            EventManager = new GameEventManager(this);
        }
        public GameDeck CreateGameDeck(int gameDeckID, Deck deck)
        {
            List<int> cardRecordIDs = new List<int>();
            foreach (Card card in deck.Cards)
            {
                cardRecordIDs.Add(GameCardManager.CreateCard(card));
            }
            GameDeck gameDeck = new GameDeck(gameDeckID, cardRecordIDs);
            gameDeck.Shuffle(100);
            return gameDeck;
        }
        private void DetectGamePlayerChangeHand(GamePlayer gamePlayer)
        {
            if(GamePlayer1.HasChangedHand && GamePlayer2.HasChangedHand)
            {
                RoundStart();
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
            RoundCount++;
            GamePlayer player = (CurrentGamePlayerID == 1) ? GamePlayer1 : GamePlayer2;
            player.ManaCrystal++;
            player.RemainedManaCrystal = player.ManaCrystal;
            player.Hero.AttackCountInThisTurn = 0;
            Field field = (CurrentGamePlayerID == 1) ? Field1 : Field2;
            foreach(var fieldCard in field.Cards)
            {
                CardRecord card;
                if(GameCardManager.FindCard(fieldCard.CardRecordID, out card) && card is ServantCardRecord)
                {
                    (card as ServantCardRecord).AttackCountInThisTurn = 0;
                }
            }
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
        public void GameOverTest()
        {
            if(GamePlayer1.Hero.RemainedHP <= 0 && GamePlayer2.Hero.RemainedHP <= 0)
            {
                OnGameOver?.Invoke(this, 0);
            }
            else if(GamePlayer1.Hero.RemainedHP <= 0)
            {
                OnGameOver?.Invoke(this, 1);
            }
            else if(GamePlayer2.Hero.RemainedHP <= 0)
            {
                OnGameOver?.Invoke(this, 2);
            }
        }
    }
}

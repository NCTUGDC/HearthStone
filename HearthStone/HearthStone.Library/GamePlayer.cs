using HearthStone.Library.CommunicationInfrastructure.Event.Managers;
using HearthStone.Library.CardRecords;
using HearthStone.Protocol;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public class GamePlayer
    {
        const int maxHandCardCount = 10;

        [MessagePackIgnore]
        public Player Player { get; set; }
        [MessagePackIgnore]
        public int GamePlayerID { get { return (Hero == null) ? -1 : Hero.HeroID; } }
        [MessagePackMember(id: 0)]
        public Hero Hero { get; private set; }

        [MessagePackMember(id: 1)]
        private bool hasChangedHand;
        [MessagePackIgnore]
        public bool HasChangedHand
        {
            get { return hasChangedHand; }
            set
            {
                hasChangedHand = value;
                OnHasChangedHandChanged?.Invoke(this);
            }
        }

        [MessagePackMember(id: 2)]
        private int remainedManaCrystal;
        [MessagePackIgnore]
        public int RemainedManaCrystal
        {
            get { return remainedManaCrystal; }
            set
            {
                remainedManaCrystal = value;
                remainedManaCrystal = Math.Max(value, 0);
                OnRemainedManaCrystalChanged?.Invoke(this);
            }
        }

        [MessagePackMember(id: 3)]
        private int manaCrystal;
        [MessagePackIgnore]
        public int ManaCrystal
        {
            get { return manaCrystal; }
            set
            {
                manaCrystal = Math.Min(value, 10);
                OnManaCrystalChanged?.Invoke(this);
            }
        }

        [MessagePackMember(id: 4)]
        private List<int> handCardIDs = new List<int>();
        [MessagePackIgnore]
        public IEnumerable<int> HandCardIDs { get { return handCardIDs; } }

        [MessagePackMember(id: 5)]
        public GameDeck Deck { get; private set; }

        [MessagePackIgnore]
        public GamePlayerEventManager EventManager { get; private set; }
        [MessagePackIgnore]
        public Game Game { get; private set; }

        public event Action<GamePlayer> OnHasChangedHandChanged;
        public event Action<GamePlayer, int, DataChangeCode> OnHandCardsChanged;
        public event Action<GamePlayer> OnRemainedManaCrystalChanged;
        public event Action<GamePlayer> OnManaCrystalChanged;

        public GamePlayer()
        {
            EventManager = new GamePlayerEventManager(this);
        }
        public GamePlayer(Player player, Hero hero, GameDeck deck)
        {
            Player = player;
            Hero = hero;
            HasChangedHand = false;
            RemainedManaCrystal = 0;
            ManaCrystal = 0;
            Deck = deck;
            EventManager = new GamePlayerEventManager(this);
        }
        public void BindGame(Game game)
        {
            Game = game;
        }
        public bool AddHandCard(int cardRecordID)
        {
            if(handCardIDs.Contains(cardRecordID))
            {
                return false;
            }
            else
            {
                if(handCardIDs.Count < maxHandCardCount)
                {
                    handCardIDs.Add(cardRecordID);
                    OnHandCardsChanged?.Invoke(this, cardRecordID, DataChangeCode.Add);
                    return true;
                }
                else
                {
                    CardRecord cardRecord;
                    if (Game != null &&  Game.GameCardManager.FindCard(cardRecordID, out cardRecord))
                    {
                        cardRecord.Destroy();
                    }
                    return false;
                }
            }
        }
        public bool RemoveHandCard(int cardRecordID)
        {
            if (handCardIDs.Contains(cardRecordID))
            {
                handCardIDs.Remove(cardRecordID);
                OnHandCardsChanged?.Invoke(this, cardRecordID, DataChangeCode.Remove);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Draw(int count)
        {
            for(int i = 0; i < count; i++)
            {
                int cardRecordID;
                if(Deck.Draw(out cardRecordID))
                {
                    AddHandCard(cardRecordID);
                }
                
            }
        }
        public void ChangeHand(int[] cardRecordIDs)
        {
            Draw(cardRecordIDs.Length);
            foreach(int selecteCardRecordID in cardRecordIDs)
            {
                RemoveHandCard(selecteCardRecordID);
                Deck.AddCard(selecteCardRecordID);
            }
            Deck.Shuffle(100);
            HasChangedHand = true;
        }
    }
}

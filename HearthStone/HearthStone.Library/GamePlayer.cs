using System;
using System.Collections.Generic;
using System.Linq;
using HearthStone.Protocol;
using MsgPack.Serialization;

namespace HearthStone.Library
{
    public class GamePlayer
    {
        [MessagePackIgnore]
        public Player Player { get; set; }
        [MessagePackIgnore]
        public int GamePlayerID { get { return Hero.HeroID; } }
        [MessagePackMember(id: 0)]
        public Hero Hero { get; private set; }

        [MessagePackMember(id: 1)]
        private bool hasChangedHand;
        [MessagePackIgnore]
        public bool HasChangedHand
        {
            get { return hasChangedHand; }
            private set
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
        [MessagePackRuntimeCollectionItemType]
        private List<CardRecord> handCards = new List<CardRecord>();
        [MessagePackIgnore]
        public IEnumerable<CardRecord> HandCards { get { return handCards; } }
        [MessagePackMember(id: 5)]
        public GameDeck Deck { get; private set; }

        public event Action<GamePlayer> OnHasChangedHandChanged;
        public event Action<CardRecord, DataChangeCode> OnHandCardsChanged;
        public event Action<GamePlayer> OnRemainedManaCrystalChanged;
        public event Action<GamePlayer> OnManaCrystalChanged;

        public GamePlayer() { }
        public GamePlayer(Player player, Hero hero, GameDeck deck)
        {
            Player = player;
            Hero = hero;
            HasChangedHand = false;
            RemainedManaCrystal = 0;
            ManaCrystal = 0;
            Deck = deck;
        }
        public void AddHandCard(CardRecord record)
        {
            handCards.Add(record);
            OnHandCardsChanged?.Invoke(record, DataChangeCode.Add);
        }
        public void RemoveHandCard(CardRecord record)
        {
            handCards.Remove(record);
            OnHandCardsChanged?.Invoke(record, DataChangeCode.Remove);
        }
        public void Draw(int count)
        {
            for(int i = 0; i < count; i++)
            {
                CardRecord card = Deck.Draw();
                AddHandCard(card);
            }
        }
        public void ChangeHand(List<int> cardRecordIDs)
        {
            Draw(cardRecordIDs.Count);
            foreach(CardRecord record in HandCards.Where(x => cardRecordIDs.Any(y => (y == x.CardRecordID))))
            {
                Deck.AddCard(record);
            }
            Deck.Shuffle(100);
            HasChangedHand = true;
        }
    }
}

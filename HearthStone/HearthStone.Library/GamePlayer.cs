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
        public Player Player { get; private set; }
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
                onHasChangedHandChanged?.Invoke(this);
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
                onRemainedManaCrystalChanged?.Invoke(this);
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
                onManaCrystalChanged?.Invoke(this);
            }
        }

        [MessagePackMember(id: 4)]
        [MessagePackRuntimeCollectionItemType]
        private List<CardRecord> handCards = new List<CardRecord>();
        [MessagePackIgnore]
        public IEnumerable<CardRecord> HandCards { get { return handCards; } }
        [MessagePackMember(id: 5)]
        public GameDeck Deck { get; private set; }

        private event Action<GamePlayer> onHasChangedHandChanged;
        public event Action<GamePlayer> OnHasChangedHandChanged { add { onHasChangedHandChanged += value; } remove { onHasChangedHandChanged -= value; } }

        private event Action<CardRecord, DataChangeCode> onHandCardsChanged;
        public event Action<CardRecord, DataChangeCode> OnHandCardsChanged { add { onHandCardsChanged += value; } remove { onHandCardsChanged -= value; } }

        private event Action<GamePlayer> onRemainedManaCrystalChanged;
        public event Action<GamePlayer> OnRemainedManaCrystalChanged { add { onRemainedManaCrystalChanged += value; } remove { onRemainedManaCrystalChanged -= value; } }

        private event Action<GamePlayer> onManaCrystalChanged;
        public event Action<GamePlayer> OnManaCrystalChanged { add { onManaCrystalChanged += value; } remove { onManaCrystalChanged -= value; } }

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
            onHandCardsChanged?.Invoke(record, DataChangeCode.Add);
        }
        public void RemoveHandCard(CardRecord record)
        {
            handCards.Remove(record);
            onHandCardsChanged?.Invoke(record, DataChangeCode.Remove);
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

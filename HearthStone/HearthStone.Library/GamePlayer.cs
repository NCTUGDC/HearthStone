using System;
using System.Collections.Generic;
using System.Linq;
using HearthStone.Protocol;

namespace HearthStone.Library
{
    public class GamePlayer
    {
        public Player Player { get; private set; }
        public Hero Hero { get; private set; }

        private bool hasChangedHand;
        public bool HasChangedHand
        {
            get { return hasChangedHand; }
            private set
            {
                hasChangedHand = value;
                onHasChangedHandChanged?.Invoke(this);
            }
        }

        private int remainedManaCrystal;
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

        private int manaCrystal;
        public int ManaCrystal
        {
            get { return manaCrystal; }
            set
            {
                manaCrystal = Math.Min(value, 10);
                onManaCrystalChanged?.Invoke(this);
            }
        }


        private List<CardRecord> handCards = new List<CardRecord>();
        public IEnumerable<CardRecord> HandCards { get { throw new NotImplementedException(); } }
        public GameDeck Deck { get; private set; }

        private event Action<GamePlayer> onHasChangedHandChanged;
        public event Action<GamePlayer> OnHasChangedHandChanged { add { onHasChangedHandChanged += value; } remove { onHasChangedHandChanged -= value; } }

        private event Action<CardRecord, DataChangeCode> onHandCardsChanged;
        public event Action<CardRecord, DataChangeCode> OnHandCardsChanged { add { onHandCardsChanged += value; } remove { onHandCardsChanged -= value; } }

        private event Action<GamePlayer> onRemainedManaCrystalChanged;
        public event Action<GamePlayer> OnRemainedManaCrystalChanged { add { onRemainedManaCrystalChanged += value; } remove { onRemainedManaCrystalChanged -= value; } }

        private event Action<GamePlayer> onManaCrystalChanged;
        public event Action<GamePlayer> OnManaCrystalChanged { add { onManaCrystalChanged += value; } remove { onManaCrystalChanged -= value; } }

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
        public void RemoveCard(CardRecord record)
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
                AddHandCard(record);
            }
            HasChangedHand = true;
        }
    }
}

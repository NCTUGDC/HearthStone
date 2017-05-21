using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public abstract class CardRecord
    {
        [MessagePackMember(id: 0)]
        public int CardRecordID { get; private set; }

        [MessagePackMember(id: 1)]
        [MessagePackRuntimeType]
        public Card Card { get; private set; }

        [MessagePackMember(id: 2)]
        [MessagePackRuntimeCollectionItemType]
        private List<Effector> effectors = new List<Effector>();
        [MessagePackIgnore]
        public IEnumerable<Effector> Effectors { get { return effectors; } }

        [MessagePackMember(id: 3)]
        private int manaCost;
        public int ManaCost
        {
            get { return manaCost; }
            set
            {
                manaCost = value;
                if (manaCost < 0)
                {
                    manaCost = 0;
                }
                onManaCostChanged?.Invoke(this);
            }
        }

        public bool IsDisplayInThisTurn { get; set; }
        public bool HasAttacked { get; set; }

        private event Action<CardRecord> onManaCostChanged;
        public event Action<CardRecord> OnManaCostChanged { add { onManaCostChanged += value; } remove { onManaCostChanged -= value; } }

        private event Action<CardRecord> onEffectorChanged;
        public event Action<CardRecord> OnEffectorChanged { add { onEffectorChanged += value; } remove { onEffectorChanged -= value; } }

        public CardRecord() { }
        protected CardRecord(int cardRecordID, Card card)
        {
            CardRecordID = cardRecordID;
            Card = card;
            ManaCost = card.ManaCost;
        }

        public void AddEffector(Effector effector)
        {
            effectors.Add(effector);
        }
        public virtual void  Reset()
        {
            effectors.Clear();
        }
    }
}

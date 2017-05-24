using HearthStone.Protocol;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public abstract class CardRecord
    {
        [MessagePackMember(id: 0)]
        public int CardRecordID { get; protected set; }

        [MessagePackMember(id: 1)]
        public int CardID { get; private set; }

        [MessagePackIgnore]
        public Card Card
        {
            get
            {
                Card card;
                if (CardManager.Instance.FindCard(CardID, out card))
                    return card;
                else
                    return null;
            }
        }

        [MessagePackMember(id: 2)]
        private List<int> effectorIDs = new List<int>();
        [MessagePackIgnore]
        public IEnumerable<int> EffectorIDs { get { return effectorIDs; } }

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
                OnManaCostChanged?.Invoke(this);
            }
        }

        public event Action<CardRecord> OnManaCostChanged;
        public event Action<CardRecord, int, DataChangeCode> OnEffectorChanged;
        public event Action<CardRecord> OnDestroyed;

        public CardRecord() { }
        protected CardRecord(int cardRecordID, int cardID)
        {
            CardRecordID = cardRecordID;
            CardID = cardID;
            ManaCost = Card.ManaCost;
        }

        public bool AddEffector(int effectorID)
        {
            if(effectorIDs.Contains(effectorID))
            {
                return false;
            }
            else
            {
                effectorIDs.Add(effectorID);
                OnEffectorChanged?.Invoke(this, effectorID, DataChangeCode.Add);
                return true;
            }
        }
        public bool RemoveEffector(int effectorID)
        {
            if (effectorIDs.Contains(effectorID))
            {
                effectorIDs.Remove(effectorID);
                OnEffectorChanged?.Invoke(this, effectorID, DataChangeCode.Remove);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Destroy()
        {
            OnDestroyed?.Invoke(this);
        }
        public IEnumerable<Effector> Effectors(GameCardManager gameCardManager)
        {
            List<Effector> efffectors = new List<Effector>();
            foreach(var effectorID in EffectorIDs)
            {
                Effector efffector;
                if (gameCardManager.FindEffector(effectorID, out efffector))
                {
                    efffectors.Add(efffector);
                }
            }
            return efffectors;
        }
    }
}

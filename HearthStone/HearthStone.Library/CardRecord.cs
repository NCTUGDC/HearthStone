using System.Collections.Generic;
using MsgPack.Serialization;

namespace HearthStone.Library
{
    public abstract class CardRecord
    {
        public int CardRecordID { get; private set; }
        [MessagePackRuntimeType]
        public Card Card { get; private set; }
        [MessagePackRuntimeCollectionItemType]
        private List<Effect> effects = new List<Effect>();

        public CardRecord() { }
        protected CardRecord(int cardRecordID, Card card)
        {
            CardRecordID = cardRecordID;
            Card = card;
        }

        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
        }
        public virtual void  Reset()
        {
            effects.Clear();
        }
    }
}

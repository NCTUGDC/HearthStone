using System.Collections.Generic;
using MsgPack.Serialization;

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

using System.Collections.Generic;

namespace HearthStone.Library
{
    public abstract class CardRecord
    {
        public int CardRecordID { get; private set; }
        public Card Card { get; private set; }
        private List<Effect> effects = new List<Effect>();

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

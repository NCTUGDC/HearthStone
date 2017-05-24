using HearthStone.Library.Cards;

namespace HearthStone.Library.CardRecords
{
    public class SpellCardRecord : CardRecord
    {
        public SpellCardRecord() { }
        public SpellCardRecord(int cardRecordID, int cardID) : base(cardRecordID, cardID)
        {
            if(!(Card is SpellCard))
            {
                CardRecordID = -1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearthStone.Protocol;
using HearthStone.Library.CardRecords;

namespace HearthStone.Library
{
    public class Field
    {
        const int maxServantCount = 7;

        private List<ServantCardRecord> cards = new List<ServantCardRecord>();
        public IEnumerable<ServantCardRecord> Cards { get { return cards; } }

        public event Action<Field> OnCardChanged;

        public Field() { }
        public bool AddServant(ServantCardRecord card, int positionIndex)
        {
            if(cards.Count < maxServantCount && positionIndex >= 0  && positionIndex <= cards.Count)
            {
                cards.Insert(positionIndex, card);
                OnCardChanged?.Invoke(this);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

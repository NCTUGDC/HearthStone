using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearthStone.Library
{
    public class GameDeck
    {
        public int GameDeckID { get; private set; }
        private List<CardRecord> cardRecords;
        public IEnumerable<CardRecord> CardRecords { get { return cardRecords.ToArray(); } }

        public GameDeck(int gameDeckID, List<CardRecord> cardRecords)
        {
            GameDeckID = gameDeckID;
            this.cardRecords = cardRecords;
        }
        public CardRecord Draw()
        {
            if(cardRecords.Count > 0)
            {
                CardRecord record = cardRecords[0];
                cardRecords.RemoveAt(0);
                return record;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}

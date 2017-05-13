using System;
using System.Collections.Generic;
using MsgPack.Serialization;

namespace HearthStone.Library
{
    public class GameDeck
    {
        [MessagePackMember(id: 0)]
        public int GameDeckID { get; private set; }
        [MessagePackMember(id: 1)]
        [MessagePackRuntimeCollectionItemType]
        private List<CardRecord> cardRecords;
        [MessagePackIgnore]
        public IEnumerable<CardRecord> CardRecords { get { return cardRecords.ToArray(); } }

        public GameDeck() { }
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
        public void AddCard(CardRecord card)
        {
            cardRecords.Add(card);
        }
        public void Shuffle(int shuffleCount)
        {
            Random randomGenerator = new Random();
            for (int i = 0; i < shuffleCount; i++)
            {
                int index1 = randomGenerator.Next(cardRecords.Count), index2 = randomGenerator.Next(cardRecords.Count);
                CardRecord record1 = cardRecords[index1], record2 = cardRecords[index2];
                cardRecords[index1] = record2;
                cardRecords[index2] = record1;
            }
        }
    }
}

using HearthStone.Protocol;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public class GameDeck
    {
        [MessagePackMember(id: 0)]
        public int GameDeckID { get; private set; }
        [MessagePackMember(id: 1)]
        private List<int> cardRecordIDs;
        [MessagePackIgnore]
        public IEnumerable<int> CardRecordIDs { get { return cardRecordIDs.ToArray(); } }

        public event Action<GameDeck, int, DataChangeCode> OnCardsChanged;
        public event Action<GameDeck, int> OnDrawCard;

        public GameDeck() { }
        public GameDeck(int gameDeckID, List<int> cardRecordIDs)
        {
            GameDeckID = gameDeckID;
            this.cardRecordIDs = cardRecordIDs;
        }
        public int Draw()
        {
            if(cardRecordIDs.Count > 0)
            {
                int recordID = cardRecordIDs[0];
                RemoveCard(recordID);
                OnDrawCard?.Invoke(this, recordID);
                return recordID;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        public void RemoveCard(int cardRecordID)
        {
            cardRecordIDs.Remove(cardRecordID);
            OnCardsChanged?.Invoke(this, cardRecordID, DataChangeCode.Remove);
        }
        public void AddCard(int cardRecordID)
        {
            cardRecordIDs.Add(cardRecordID);
            OnCardsChanged?.Invoke(this, cardRecordID, DataChangeCode.Add);
        }
        public void Shuffle(int shuffleCount)
        {
            Random randomGenerator = new Random();
            for (int i = 0; i < shuffleCount; i++)
            {
                int index1 = randomGenerator.Next(cardRecordIDs.Count), index2 = randomGenerator.Next(cardRecordIDs.Count);
                int record1ID = cardRecordIDs[index1], record2ID = cardRecordIDs[index2];
                cardRecordIDs[index1] = record2ID;
                cardRecordIDs[index2] = record1ID;
            }
        }
    }
}

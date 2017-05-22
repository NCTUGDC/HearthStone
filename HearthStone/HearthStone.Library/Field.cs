using HearthStone.Library.CommunicationInfrastructure.Event.Managers;
using HearthStone.Protocol;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public class Field
    {
        public class FieldCardRecord
        {
            [MessagePackMember(id: 0)]
            public int CardRecordID { get; set; }
            [MessagePackMember(id: 1)]
            public int PositionIndex { get; set; }
        }
        const int maxServantCount = 7;

        [MessagePackMember(id: 0)]
        public int FieldID { get; private set; }

        [MessagePackMember(id: 1)]
        private Dictionary<int, FieldCardRecord> cardDictionary = new Dictionary<int, FieldCardRecord>();
        public IEnumerable<FieldCardRecord> Cards { get { return cardDictionary.Values; } }
        [MessagePackIgnore]
        public int ServantCount { get { return cardDictionary.Count; } }

        [MessagePackIgnore]
        public FieldEventManager EventManager { get; private set; }
        [MessagePackIgnore]
        public Game Game { get; private set; }

        public event Action<FieldCardRecord, DataChangeCode> OnCardChanged;

        public Field()
        {
            EventManager = new FieldEventManager(this);
        }
        public Field(int fieldID)
        {
            FieldID = fieldID;
            EventManager = new FieldEventManager(this);
        }
        public bool AddCard(int cardRecordID, int positionIndex)
        {
            if(cardDictionary.Count < maxServantCount && positionIndex >= 0  && positionIndex <= cardDictionary.Count)
            {
                FieldCardRecord record = new FieldCardRecord { CardRecordID = cardRecordID, PositionIndex = positionIndex };
                foreach(var targetCard in Cards)
                {
                    if(targetCard.PositionIndex >= positionIndex)
                    {
                        targetCard.PositionIndex++;
                        OnCardChanged?.Invoke(targetCard, DataChangeCode.Update);
                    }
                }
                cardDictionary.Add(cardRecordID, record);
                OnCardChanged?.Invoke(record, DataChangeCode.Add);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RemoveCard(int cardRecordID)
        {
            if(cardDictionary.ContainsKey(cardRecordID))
            {
                FieldCardRecord record = cardDictionary[cardRecordID];
                cardDictionary.Remove(record.CardRecordID);
                OnCardChanged?.Invoke(record, DataChangeCode.Remove);
                foreach (var targetCard in Cards)
                {
                    if (targetCard.PositionIndex >= record.PositionIndex)
                    {
                        targetCard.PositionIndex--;
                        OnCardChanged?.Invoke(targetCard, DataChangeCode.Update);
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateCard(int cardRecordID, int positionIndex)
        {
            if (cardDictionary.ContainsKey(cardRecordID))
            {
                FieldCardRecord record = cardDictionary[cardRecordID];
                record.PositionIndex = positionIndex;
                OnCardChanged?.Invoke(record, DataChangeCode.Update);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void BindGame(Game game)
        {
            Game = game;
        }
        public bool DisplayCheck(int positionIndex)
        {
            if (ServantCount >= maxServantCount)
                return false;
            else if (positionIndex < 0 || positionIndex >= ServantCount)
                return false;
            else
                return true;
        }
    }
}

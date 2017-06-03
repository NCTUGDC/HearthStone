using HearthStone.Library.CommunicationInfrastructure.Event.Managers;
using HearthStone.Library.Effectors;
using HearthStone.Protocol;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private Dictionary<int, FieldCardRecord> fieldCardDictionary = new Dictionary<int, FieldCardRecord>();
        public IEnumerable<FieldCardRecord> FieldCards { get { return fieldCardDictionary.Values.ToArray(); } }
        [MessagePackIgnore]
        public int ServantCount { get { return fieldCardDictionary.Count; } }

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
            if (fieldCardDictionary.ContainsKey(cardRecordID))
                return false;
            if (DisplayCheck(positionIndex))
            {
                FieldCardRecord record = new FieldCardRecord { CardRecordID = cardRecordID, PositionIndex = positionIndex };
                foreach(var targetCard in FieldCards)
                {
                    if(targetCard.PositionIndex >= positionIndex)
                    {
                        targetCard.PositionIndex++;
                        OnCardChanged?.Invoke(targetCard, DataChangeCode.Update);
                    }
                }
                fieldCardDictionary.Add(cardRecordID, record);
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
            if(fieldCardDictionary.ContainsKey(cardRecordID))
            {
                FieldCardRecord record = fieldCardDictionary[cardRecordID];
                fieldCardDictionary.Remove(record.CardRecordID);
                OnCardChanged?.Invoke(record, DataChangeCode.Remove);
                foreach (var targetCard in FieldCards)
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
            if (fieldCardDictionary.ContainsKey(cardRecordID))
            {
                FieldCardRecord record = fieldCardDictionary[cardRecordID];
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
            else if (positionIndex < 0 || positionIndex > ServantCount)
                return false;
            else
                return true;
        }
        public IEnumerable<CardRecord> Cards(GameCardManager gameCardManager)
        {
            List<CardRecord> cards = new List<CardRecord>();
            foreach (var fieldCard in FieldCards)
            {
                CardRecord cardRecord;
                if (gameCardManager.FindCard(fieldCard.CardRecordID, out cardRecord))
                {
                    cards.Add(cardRecord);
                }
            }
            return cards;
        }
        public bool AnyTauntServant()
        {
            return Cards(Game.GameCardManager).Any(x => x.Effectors(Game.GameCardManager).Any(y => y is TauntEffector));
        }
        public bool FindCardWithPositionIndex(int positionIndex, out int cardRecordID)
        {
            if(fieldCardDictionary.Values.Any(x => x.PositionIndex == positionIndex))
            {
                cardRecordID = fieldCardDictionary.Values.First(x => x.PositionIndex == positionIndex).CardRecordID;
                return true;
            }
            {
                cardRecordID = 0;
                return false;
            }
        }
    }
}

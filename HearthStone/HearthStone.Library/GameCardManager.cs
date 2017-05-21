using HearthStone.Protocol;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public class GameCardManager
    {
        [MessagePackIgnore]
        public Game Game { get; private set; }
        [MessagePackIgnore]
        private int cardRecordID_Generator = 1;
        [MessagePackIgnore]
        public int NewCardRecordID { get { return cardRecordID_Generator++; } }

        [MessagePackIgnore]
        private int effectorID_Generator = 1;
        [MessagePackIgnore]
        public int NewEffectorID { get { return effectorID_Generator++; } }

        [MessagePackMember(id: 0)]
        [MessagePackRuntimeCollectionItemType]
        private Dictionary<int, CardRecord> cardDictionary = new Dictionary<int, CardRecord>();
        [MessagePackMember(id: 1)]
        [MessagePackRuntimeCollectionItemType]
        private Dictionary<int, Effector> effectorDictionary = new Dictionary<int, Effector>();

        public event Action<CardRecord, DataChangeCode> OnCardChanged;
        public event Action<Effector, DataChangeCode> OnEffectorChanged;

        public GameCardManager() { }

        public void BindGame(Game game)
        {
            Game = game;
        }

        public bool FindCard(int cardRecordID, out CardRecord card)
        {
            if(cardDictionary.ContainsKey(cardRecordID))
            {
                card = cardDictionary[cardRecordID];
                return true;
            }
            else
            {
                card = null;
                return false;
            }
        }
        public int CreateCard(Card card)
        {
            CardRecord record = card.CreateRecord(NewCardRecordID);
            LoadCard(record);
            return record.CardRecordID;
        }
        public void LoadCard(CardRecord card)
        {
            if(cardDictionary.ContainsKey(card.CardRecordID))
            {
                cardDictionary[card.CardRecordID] = card;
                OnCardChanged?.Invoke(card, DataChangeCode.Update);
            }
            else
            {
                cardDictionary.Add(card.CardRecordID, card);
                OnCardChanged?.Invoke(card, DataChangeCode.Add);
            }
        }

        public bool FindEffector(int effectorID, out Effector effector)
        {
            if (effectorDictionary.ContainsKey(effectorID))
            {
                effector = effectorDictionary[effectorID];
                return true;
            }
            else
            {
                effector = null;
                return false;
            }
        }
        public int CreateEffector(Effect effect)
        {
            Effector effector = effect.CreateEffector(NewEffectorID);
            LoadEffector(effector);
            return effector.EffectorID;
        }
        public void LoadEffector(Effector effector)
        {
            if (effectorDictionary.ContainsKey(effector.EffectorID))
            {
                effectorDictionary[effector.EffectorID] = effector;
                OnEffectorChanged?.Invoke(effector, DataChangeCode.Update);
            }
            else
            {
                effectorDictionary.Add(effector.EffectorID, effector);
                OnEffectorChanged?.Invoke(effector, DataChangeCode.Add);
            }
        }

        public void RegisterCardRecordEvents(CardRecord record)
        {
            
        }
    }
}

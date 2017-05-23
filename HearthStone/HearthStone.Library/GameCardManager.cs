using HearthStone.Library.CardRecords;
using HearthStone.Library.Effectors;
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
        private int effectorID_Generator = 1;

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
        public CardRecord CreateCardRecord(Card card)
        {
            CardRecord record;
            switch(card.CardType)
            {
                case CardTypeCode.Servant:
                    record = new ServantCardRecord(cardRecordID_Generator++, card.CardID);
                    break;
                case CardTypeCode.Spell:
                    record = new SpellCardRecord(cardRecordID_Generator++, card.CardID);
                    break;
                case CardTypeCode.Weapon:
                    record = new WeaponCardRecord(cardRecordID_Generator++, card.CardID);
                    break;
                default:
                    return null;
            }
            LoadCard(record);
            foreach(Effect effect in card.Effects)
            {
                record.AddEffector(CreareEffector(effect).EffectorID);
            }
            return record;
        }
        public Effector CreareEffector(Effect effect)
        {
            Effector effector = null;
            switch (effect.EffectType)
            {
                case EffectTypeCode.Taunt:
                    effector = new TauntEffector(effectorID_Generator++, effect.EffectID);
                    break;
                case EffectTypeCode.Charge:
                    effector = new ChargeEffector(effectorID_Generator++, effect.EffectID);
                    break;
                default:
                    return null;
            }
            LoadEffector(effector);
            return effector;
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

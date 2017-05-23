using HearthStone.Library.Cards;
using MsgPack.Serialization;
using System;

namespace HearthStone.Library.CardRecords
{
    public class WeaponCardRecord : CardRecord
    {
        [MessagePackMember(id: 4)]
        private int attack;
        public int Attack
        {
            get { return attack; }
            set
            {
                attack = Math.Max(value, 0);
                OnAttackChanged?.Invoke(this);
            }
        }
        [MessagePackMember(id: 5)]
        private int durability;
        public int Durability
        {
            get { return durability; }
            set
            {
                durability = value;
                OnDurabilityChanged?.Invoke(this);
            }
        }
        [MessagePackMember(id: 6)]
        private int remainedDurability;
        public int RemainedDurability
        {
            get { return remainedDurability; }
            set
            {
                remainedDurability = Math.Min(value, Durability);
                OnRemainedDurabilityChanged?.Invoke(this);
            }
        }

        public event Action<WeaponCardRecord> OnAttackChanged;
        public event Action<WeaponCardRecord> OnDurabilityChanged;
        public event Action<WeaponCardRecord> OnRemainedDurabilityChanged;

        public WeaponCardRecord() { }
        public WeaponCardRecord(int cardRecordID, int cardID) : base(cardRecordID, cardID)
        {
            if (Card is WeaponCard)
            {
                WeaponCard weaponCard = Card as WeaponCard;
                Attack = weaponCard.Attack;
                Durability = weaponCard.Durability;
                RemainedDurability = Durability;
            }
            else
            {
                LogService.Fatal($"CradID: {cardID} is used to create WeaponCardRecord");
            }
        }
    }
}

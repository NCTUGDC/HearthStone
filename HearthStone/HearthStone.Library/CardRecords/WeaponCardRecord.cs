using HearthStone.Library.Cards;
using System;
using MsgPack.Serialization;

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
                onAttackChanged?.Invoke(this);
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
                onDurabilityChanged?.Invoke(this);
            }
        }

        private event Action<WeaponCardRecord> onAttackChanged;
        public event Action<WeaponCardRecord> OnAttackChanged { add { onAttackChanged += value; } remove { onAttackChanged -= value; } }

        private event Action<WeaponCardRecord> onDurabilityChanged;
        public event Action<WeaponCardRecord> OnDurabilityChanged { add { onDurabilityChanged += value; } remove { onDurabilityChanged -= value; } }

        public WeaponCardRecord() { }
        public WeaponCardRecord(int cardRecordID, Card card) : base(cardRecordID, card)
        {
            if(card is WeaponCard)
            {
                WeaponCard weaponCard = card as WeaponCard;
                Attack = weaponCard.Attack;
                Durability = weaponCard.Durability;
            }
            else
            {
                LogService.Fatal($"CradID: {card.CardID} is used to create WeaponCardRecord");
            }
        }
        public override void Reset()
        {
            base.Reset();
            if (Card is WeaponCard)
            {
                WeaponCard weaponCard = Card as WeaponCard;
                if(Attack > weaponCard.Attack)
                    Attack = weaponCard.Attack;
                if (Durability > weaponCard.Durability)
                    Durability = weaponCard.Durability;
            }
            else
            {
                LogService.Fatal($"CradID: {Card.CardID} is used to Reset WeaponCardRecord");
            }
        }
    }
}

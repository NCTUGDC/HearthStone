using HearthStone.Library.Cards;
using MsgPack.Serialization;
using System;

namespace HearthStone.Library.CardRecords
{
    public class ServantCardRecord : CardRecord
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
        private int health;
        public int Health
        {
            get { return health; }
            set
            {
                health = Math.Max(value, 0);
                onHealthChanged?.Invoke(this);
            }
        }

        [MessagePackMember(id: 6)]
        private int remainedHealth;
        public int RemainedHealth
        {
            get { return remainedHealth; }
            set
            {
                remainedHealth = Math.Min(value, Health);
                onRemainedHealthChanged?.Invoke(this);
            }
        }


        private event Action<ServantCardRecord> onAttackChanged;
        public event Action<ServantCardRecord> OnAttackChanged { add { onAttackChanged += value; } remove { onAttackChanged -= value; } }

        private event Action<ServantCardRecord> onHealthChanged;
        public event Action<ServantCardRecord> OnHealthChanged { add { onHealthChanged += value; } remove { onHealthChanged -= value; } }

        private event Action<ServantCardRecord> onRemainedHealthChanged;
        public event Action<ServantCardRecord> OnRemainedHealthChanged { add { onRemainedHealthChanged += value; } remove { onRemainedHealthChanged -= value; } }

        public ServantCardRecord() { }
        public ServantCardRecord(int cardRecordID, Card card) : base(cardRecordID, card)
        {
            if (card is ServantCard)
            {
                ServantCard servantCard = card as ServantCard;
                Attack = servantCard.Attack;
                Health = servantCard.Health;
                RemainedHealth = Health;
            }
            else
            {
                LogService.Fatal($"CradID: {card.CardID} is used to create ServantCardRecord");
            }
        }
        public override void Reset()
        {
            base.Reset();
            if (Card is ServantCard)
            {
                ServantCard servantCard = Card as ServantCard;
                if (Attack > servantCard.Attack)
                    Attack = servantCard.Attack;
                if (Health > servantCard.Health)
                    Health = servantCard.Health;
            }
            else
            {
                LogService.Fatal($"CradID: {Card.CardID} is used to Reset ServantCardRecord");
            }
        }
    }
}

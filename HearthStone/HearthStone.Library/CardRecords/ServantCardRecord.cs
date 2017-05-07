using HearthStone.Library.Cards;
using System;

namespace HearthStone.Library.CardRecords
{
    public class ServantCardRecord : CardRecord
    {
        private int attack;
        public int Attack
        {
            get { return attack; }
            set
            {
                attack = value;
                if (attack < 0)
                {
                    attack = 0;
                }
                onAttackChanged?.Invoke(this);
            }
        }
        private int health;
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                onHealthChanged?.Invoke(this);
            }
        }

        private event Action<ServantCardRecord> onAttackChanged;
        public event Action<ServantCardRecord> OnAttackChanged { add { onAttackChanged += value; } remove { onAttackChanged -= value; } }

        private event Action<ServantCardRecord> onHealthChanged;
        public event Action<ServantCardRecord> OnHealthChanged { add { onHealthChanged += value; } remove { onHealthChanged -= value; } }

        public ServantCardRecord(int cardRecordID, Card card) : base(cardRecordID, card)
        {
            if (card is ServantCard)
            {
                ServantCard servantCard = card as ServantCard;
                Attack = servantCard.Attack;
                Health = servantCard.Health;
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

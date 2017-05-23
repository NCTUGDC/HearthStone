using HearthStone.Library.Cards;
using HearthStone.Library.Effectors;
using MsgPack.Serialization;
using System;
using System.Linq;

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
                OnAttackChanged?.Invoke(this);
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
                OnHealthChanged?.Invoke(this);
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
                OnRemainedHealthChanged?.Invoke(this);
            }
        }
        [MessagePackMember(id: 7)]
        private bool isDisplayInThisTurn;
        public bool IsDisplayInThisTurn
        {
            get { return isDisplayInThisTurn; }
            set
            {
                isDisplayInThisTurn = value;
                OnIsDisplayInThisTurnChanged?.Invoke(this);
            }
        }
        [MessagePackMember(id: 8)]
        private int attackCountInThisTurn;
        public int AttackCountInThisTurn
        {
            get { return attackCountInThisTurn; }
            set
            {
                attackCountInThisTurn = value;
                OnAttackCountInThisTurnChanged?.Invoke(this);
            }
        }

        public event Action<ServantCardRecord> OnAttackChanged;
        public event Action<ServantCardRecord> OnHealthChanged;
        public event Action<ServantCardRecord> OnRemainedHealthChanged;
        public event Action<CardRecord> OnIsDisplayInThisTurnChanged;
        public event Action<CardRecord> OnAttackCountInThisTurnChanged;

        public ServantCardRecord() { }
        public ServantCardRecord(int cardRecordID, int cardID) : base(cardRecordID, cardID)
        {
            if (Card is ServantCard)
            {
                ServantCard servantCard = Card as ServantCard;
                Attack = servantCard.Attack;
                Health = servantCard.Health;
                RemainedHealth = Health;
            }
            else
            {
                LogService.Fatal($"CradID: {cardID} is used to create ServantCardRecord");
            }
        }
        public override void Reset()
        {
            base.Reset();
            if (Card is ServantCard)
            {
                ServantCard servantCard = Card as ServantCard;
                Attack = servantCard.Attack;
                Health = servantCard.Health;
                RemainedHealth = Health;
            }
            else
            {
                LogService.Fatal($"CradID: {Card.CardID} is used to Reset ServantCardRecord");
            }
        }

        public bool AttackServant(ServantCardRecord target, GamePlayer user)
        {
            bool hasCharge = Effectors(user.Game.GameCardManager).Any(x => x is ChargeEffector);
            bool hasWindfury = Effectors(user.Game.GameCardManager).Any(x => x is WindfuryEffector);
            if ((AttackCountInThisTurn < 1 ||(hasWindfury && AttackCountInThisTurn < 2)) && (!IsDisplayInThisTurn || hasCharge) && Attack > 0)
            {
                Field opponentField = user.Game.OpponentField(user.GamePlayerID);
                if (opponentField.AnyTauntServant())
                {
                    if (target.Effectors(user.Game.GameCardManager).Any(x => x is TauntEffector))
                    {
                        target.RemainedHealth -= Attack;
                        RemainedHealth -= target.Attack;
                        AttackCountInThisTurn++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    target.RemainedHealth -= Attack;
                    RemainedHealth -= target.Attack;
                    AttackCountInThisTurn++;
                    return true;
                } 
            }
            else
            {
                return false;
            }
        }
        public bool AttackHero(Hero target, GamePlayer user)
        {
            bool hasCharge = Effectors(user.Game.GameCardManager).Any(x => x is ChargeEffector);
            bool hasWindfury = Effectors(user.Game.GameCardManager).Any(x => x is WindfuryEffector);
            if ((AttackCountInThisTurn < 1 || (hasWindfury && AttackCountInThisTurn < 2)) && (!IsDisplayInThisTurn || hasCharge) && Attack > 0)
            {
                Field opponentField = user.Game.OpponentField(user.GamePlayerID);
                if (opponentField.AnyTauntServant())
                {
                    return false;
                }
                else
                {
                    target.RemainedHP -= Attack;
                    RemainedHealth -= target.AttackWithWeapon(user.Game);
                    AttackCountInThisTurn++;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

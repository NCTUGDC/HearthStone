using HearthStone.Library.CardRecords;
using HearthStone.Protocol;
using MsgPack.Serialization;
using System;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public class Hero
    {
        [MessagePackMember(id: 0)]
        public int HeroID { get; private set; }

        [MessagePackMember(id: 1)]
        private int weaponCardRecordID;
        public int WeaponCardRecordID
        {
            get { return weaponCardRecordID; }
            set
            {
                if(weaponCardRecordID != 0)
                {
                    OnWeaponChanged?.Invoke(this, DataChangeCode.Remove);
                }
                weaponCardRecordID = value;
                OnWeaponChanged?.Invoke(this, DataChangeCode.Add);
            }
        }

        [MessagePackMember(id: 2)]
        private int attack;
        public int Attack
        {
            get { return attack; }
            set
            {
                int oldValue = attack;
                attack = Math.Max(value, 0);
                OnAttackChanged?.Invoke(this, attack - oldValue);
            }
        }

        [MessagePackMember(id: 3)]
        private int hp;
        public int HP
        {
            get { return hp; }
            set
            {
                int oldValue = hp;
                hp = Math.Max(value, 0);
                OnHP_Changed?.Invoke(this, hp - oldValue);
            }
        }

        [MessagePackMember(id: 4)]
        private int remainedHP;
        public int RemainedHP
        {
            get { return remainedHP; }
            set
            {
                int oldValue = remainedHP;
                remainedHP = Math.Min(value, HP);
                OnRemainedHP_Changed?.Invoke(this, remainedHP - oldValue);
            }
        }

        [MessagePackMember(id: 5)]
        [MessagePackRuntimeCollectionItemType]
        private List<int> effectorIDs = new List<int>();

        [MessagePackIgnore]
        public IEnumerable<int> EffectorIDs { get { return effectorIDs; } }

        [MessagePackMember(id: 6)]
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

        public event Action<Hero, DataChangeCode> OnWeaponChanged;
        public delegate void HeroValueChangedEventHandler(Hero hero, int delta);
        public event HeroValueChangedEventHandler OnAttackChanged;
        public event HeroValueChangedEventHandler OnRemainedHP_Changed;
        public event HeroValueChangedEventHandler OnHP_Changed;
        public event Action<Hero, int, DataChangeCode> OnEffectorChanged;
        public event Action<Hero> OnAttackCountInThisTurnChanged;

        public Hero() { }
        public Hero(int heroID, int remainedHP, int hp)
        {
            HeroID = heroID;
            HP = hp;
            RemainedHP = remainedHP;
            AttackCountInThisTurn = 0;
        }
        public bool AddEffector(int effectorID)
        {
            if(effectorIDs.Contains(effectorID))
            {
                return false;
            }
            else
            {
                effectorIDs.Add(effectorID);
                OnEffectorChanged?.Invoke(this, effectorID, DataChangeCode.Add);
                return true;
            }
        }
        public bool RemoveEffector(int effectorID)
        {
            if (effectorIDs.Contains(effectorID))
            {
                effectorIDs.Remove(effectorID);
                OnEffectorChanged?.Invoke(this, effectorID, DataChangeCode.Remove);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AttackServant(ServantCardRecord target, GamePlayer user)
        {
            if (AttackWithWeapon(user.Game) > 0)
            {
                target.RemainedHealth -= AttackWithWeapon(user.Game);
                RemainedHP -= target.Attack;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AttackHero(Hero target, GamePlayer user)
        {
            if (AttackWithWeapon(user.Game) > 0)
            {
                target.RemainedHP -= AttackWithWeapon(user.Game);
                RemainedHP -= target.AttackWithWeapon(user.Game);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int AttackWithWeapon(Game game)
        {
            CardRecord card;
            if (game.CurrentGamePlayerID == HeroID && game.GameCardManager.FindCard(WeaponCardRecordID, out card) && (card is WeaponCardRecord))
            {
                WeaponCardRecord weaponCard = card as WeaponCardRecord;
                return Attack + weaponCard.Attack;
            }
            else
            {
                return Attack;
            }
        }
    }
}

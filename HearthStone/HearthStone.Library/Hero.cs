using MsgPack.Serialization;
using System.Collections.Generic;
using HearthStone.Library.CardRecords;

namespace HearthStone.Library
{
    public class Hero
    {
        public int HeroID { get; private set; }
        public WeaponCardRecord Weapon { get; set; }
        public int Attack { get; set; }
        public int RemainedHP { get; set; }
        public int HP { get; set; }
        [MessagePackRuntimeCollectionItemType]
        private List<Effector> effectors = new List<Effector>();
        [MessagePackIgnore]
        public IEnumerable<Effector> Effectors { get { return effectors; } }
        public bool HasAttacked { get; set; }


        public Hero() { }
        public Hero(int heroID, int remainedHP, int hp)
        {
            HeroID = heroID;
            RemainedHP = remainedHP;
            HP = hp;
        }
        public void AddEffector(Effector effector)
        {
            effectors.Add(effector);
        }
        public virtual void Reset()
        {
            effectors.Clear();
        }
    }
}

using HearthStone.Library.CardRecords;

namespace HearthStone.Library.Effectors
{
    public abstract class TargetEffector : Effector
    {
        public TargetEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public abstract void AffectServant(ServantCardRecord servant, GamePlayer user);
        public abstract void AffectHero(Hero hero, GamePlayer user);
    }
}

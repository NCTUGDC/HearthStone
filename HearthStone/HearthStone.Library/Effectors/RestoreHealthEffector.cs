using HearthStone.Library.CardRecords;
using System;

namespace HearthStone.Library.Effectors
{
    public class RestoreHealthEffector : TargetEffector
    {
        public RestoreHealthEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectHero(Hero hero, GamePlayer user)
        {
            throw new NotImplementedException();
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            throw new NotImplementedException();
        }
    }
}

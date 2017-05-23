using System;
using HearthStone.Library.CardRecords;

namespace HearthStone.Library.Effectors
{
    public class DoubleMinionHealthEffector : MinionTargetEffector
    {
        public DoubleMinionHealthEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            throw new NotImplementedException();
        }
    }
}

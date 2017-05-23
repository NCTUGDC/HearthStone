using HearthStone.Library.CardRecords;
using System;

namespace HearthStone.Library.Effectors
{
    public class GiveMinionHealthBuffEffector : MinionTargetEffector
    {
        public GiveMinionHealthBuffEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            throw new NotImplementedException();
        }
    }
}

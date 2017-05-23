using System;
using System.Collections.Generic;
using HearthStone.Library.CardRecords;
namespace HearthStone.Library.Effectors
{
    public abstract class MinionTargetEffector : Effector
    {
        public MinionTargetEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public abstract void AffectServant(ServantCardRecord servant, GamePlayer user);
    }
}

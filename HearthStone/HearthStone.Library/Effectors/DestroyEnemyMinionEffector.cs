﻿using HearthStone.Library.CardRecords;
using System;

namespace HearthStone.Library.Effectors
{
    public class DestroyEnemyMinionEffector : MinionTargetEffector
    {
        public DestroyEnemyMinionEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            throw new NotImplementedException();
        }
    }
}

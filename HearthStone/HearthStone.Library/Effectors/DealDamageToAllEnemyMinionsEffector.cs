using System;

namespace HearthStone.Library.Effectors
{
    public class DealDamageToAllEnemyMinionsEffector : AutoExecutetEffector
    {
        public DealDamageToAllEnemyMinionsEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void Affect(GamePlayer user)
        {
            throw new NotImplementedException();
        }
    }
}

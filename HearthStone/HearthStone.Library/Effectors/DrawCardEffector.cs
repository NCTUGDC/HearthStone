using System;

namespace HearthStone.Library.Effectors
{
    public class DrawCardEffector : AutoExecutetEffector
    {
        public DrawCardEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void Affect(GamePlayer user)
        {
            throw new NotImplementedException();
        }
    }
}

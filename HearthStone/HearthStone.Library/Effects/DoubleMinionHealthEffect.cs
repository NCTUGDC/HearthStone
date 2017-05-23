using System;
using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class DoubleMinionHealthEffect : Effect
    {
        public DoubleMinionHealthEffect(int effectID) : base(effectID)
        {
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            throw new NotImplementedException();
        }
    }
}

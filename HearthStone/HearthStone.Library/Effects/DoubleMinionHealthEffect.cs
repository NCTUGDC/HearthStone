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
                return EffectTypeCode.DoubleMinionHealth;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "使一個手下的生命值加倍";
        }
    }
}

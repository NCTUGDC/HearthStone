using HearthStone.Protocol;
using System;

namespace HearthStone.Library.Effects
{
    public class WindfuryEffect : Effect
    {
        public WindfuryEffect(int effectID) : base(effectID)
        {
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.Windfury;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "風怒";
        }
    }
}

using HearthStone.Protocol;
using System;

namespace HearthStone.Library.Effects
{
    public class GiveMinionHealthBuffEffect : Effect
    {
        public int BuffNumber { get; private set; }
        public GiveMinionHealthBuffEffect(int effectID, int buffNumber) : base(effectID)
        {
            BuffNumber = buffNumber;
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

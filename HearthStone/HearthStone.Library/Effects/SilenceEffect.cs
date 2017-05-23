using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class SilenceEffect : Effect
    {
        public SilenceEffect(int effectID) : base(effectID)
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

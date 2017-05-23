using HearthStone.Protocol;
using System;

namespace HearthStone.Library.Effects
{
    public class RestoreHealthEffect : Effect
    {
        public int RestoreNumber { get; private set; }
        public RestoreHealthEffect(int effectID, int restoreNumber) : base(effectID)
        {
            RestoreNumber = restoreNumber;
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

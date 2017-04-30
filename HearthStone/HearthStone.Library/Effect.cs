using HearthStone.Protocol;
using System;

namespace HearthStone.Library
{
    public abstract class Effect
    {
        public int EffectID { get; private set; }
        public abstract EffectTypeCode EffectType { get; }
        public string Description
        {
            get
            {
                throw new NotImplementedException("Effect Description");
            }
        }

        protected Effect(int effectID)
        {
            EffectID = effectID;
        }
    }
}

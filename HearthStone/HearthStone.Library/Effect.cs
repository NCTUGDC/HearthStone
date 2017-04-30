using HearthStone.Protocol;
using System;

namespace HearthStone.Library
{
    public abstract class Effect
    {
        public int EffectID { get; private set; }
        public abstract EffectTypeCode EffectType { get; }
        public abstract string Description { get; }

        protected Effect(int effectID)
        {
            throw new NotImplementedException("Effect Constructor");
        }
    }
}

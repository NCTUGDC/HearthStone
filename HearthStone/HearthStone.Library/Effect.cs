using HearthStone.Protocol;

namespace HearthStone.Library
{
    public abstract class Effect
    {
        public int EffectID { get; private set; }
        public abstract EffectTypeCode EffectType { get; }
        public abstract string Description(Game game, int selfGamePlayerID);

        protected Effect(int effectID)
        {
            EffectID = effectID;
        }
    }
}

using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class SilenceMinionEffect : Effect
    {
        public SilenceMinionEffect(int effectID) : base(effectID)
        {
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.SilenceMinion;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "沉默一個手下";
        }
    }
}

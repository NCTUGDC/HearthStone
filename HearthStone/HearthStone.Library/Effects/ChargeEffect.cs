using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class ChargeEffect : Effect
    {
        public ChargeEffect(int effectID) : base(effectID)
        {
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.Charge;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "衝鋒";
        }
    }
}

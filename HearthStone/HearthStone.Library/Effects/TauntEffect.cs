using HearthStone.Library.Effectors;
using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class TauntEffect : Effect
    {
        public TauntEffect(int effectID) : base(effectID)
        {
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.Taunt;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "嘲諷";
        }
    }
}

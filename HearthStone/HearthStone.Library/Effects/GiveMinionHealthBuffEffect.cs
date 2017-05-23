using HearthStone.Protocol;

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
                return EffectTypeCode.GiveMinionHealthBuff;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return $"賦予一個手下+{BuffNumber}生命";
        }
    }
}

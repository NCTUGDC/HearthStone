using HearthStone.Protocol;

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
                return EffectTypeCode.RestoreHealth;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return $"恢復{RestoreNumber}點生命值";
        }
    }
}

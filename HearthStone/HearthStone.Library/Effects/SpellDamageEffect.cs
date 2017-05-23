using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class SpellDamageEffect : Effect
    {
        public int Damage { get; private set; }
        public SpellDamageEffect(int effectID, int damage) : base(effectID)
        {
            Damage = damage;
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.SpellDamage;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return $"法術傷害+{Damage}";
        }
    }
}

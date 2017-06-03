using HearthStone.Library.Effects;

namespace HearthStone.Library.Effectors
{
    public class SpellDamageEffector : StatusEffector
    {
        public SpellDamageEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public int SpellDamage()
        {
            Effect effect = Effect;
            if (effect != null && effect is SpellDamageEffect)
            {
                return (effect as SpellDamageEffect).Damage;
            }
            else
            {
                return 0;
            }
        }
    }
}

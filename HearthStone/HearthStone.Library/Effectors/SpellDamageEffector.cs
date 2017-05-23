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
            Effect effect;
            if(CardManager.Instance.FindEffect(EffectID, out effect) && effect is SpellDamageEffect)
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

using HearthStone.Library.CardRecords;
using HearthStone.Library.Effects;
using System.Linq;

namespace HearthStone.Library.Effectors
{
    public class DealSpellDamageEffector : TargetEffector
    {
        public DealSpellDamageEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectHero(Hero hero, GamePlayer user)
        {
            Effect effect = Effect;
            if (effect != null && effect is DealSpellDamageEffect)
            {
                int extraSpellDamage = user.Game.SelfField(user.GamePlayerID).Cards(user.Game.GameCardManager).Sum(x => x.Effectors(user.Game.GameCardManager).OfType<SpellDamageEffector>().Sum(y => y.SpellDamage()));
                hero.RemainedHP -= (effect as DealSpellDamageEffect).Damage + extraSpellDamage;
            }
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            Effect effect = Effect;
            if (effect != null && effect is DealSpellDamageEffect)
            {
                int extraSpellDamage1 = user.Game.SelfField(user.GamePlayerID).Cards(user.Game.GameCardManager).Sum(x => x.Effectors(user.Game.GameCardManager).OfType<SpellDamageEffector>().Sum(y => y.SpellDamage()));
                int extraSpellDamage2 = (user.Hero.WeaponCardRecordID != 0) ? user.Hero.Weapon(user.Game.GameCardManager).Effectors(user.Game.GameCardManager).OfType<SpellDamageEffector>().Sum(y => y.SpellDamage()) : 0;
                servant.RemainedHealth -= (effect as DealSpellDamageEffect).Damage + extraSpellDamage1 + extraSpellDamage2;
            }
        }
    }
}

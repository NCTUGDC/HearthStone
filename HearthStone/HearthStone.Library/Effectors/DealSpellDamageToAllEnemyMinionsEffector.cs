using HearthStone.Library.CardRecords;
using HearthStone.Library.Effects;
using System.Linq;

namespace HearthStone.Library.Effectors
{
    public class DealSpellDamageToAllEnemyMinionsEffector : AutoExecutetEffector
    {
        public DealSpellDamageToAllEnemyMinionsEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void Affect(GamePlayer user)
        {
            Effect effect = Effect;
            if (effect != null && effect is DealSpellDamageToAllEnemyMinionsEffect)
            {
                int extraSpellDamage1 = user.Game.SelfField(user.GamePlayerID).Cards(user.Game.GameCardManager).Sum(x => x.Effectors(user.Game.GameCardManager).OfType<SpellDamageEffector>().Sum(y => y.SpellDamage()));
                int extraSpellDamage2 = user.Hero.Weapon(user.Game.GameCardManager).Effectors(user.Game.GameCardManager).OfType<SpellDamageEffector>().Sum(y => y.SpellDamage());
                int damage = (effect as DealSpellDamageToAllEnemyMinionsEffect).Damage + extraSpellDamage1 + extraSpellDamage2;
                foreach(var fieldCard in user.Game.OpponentField(user.GamePlayerID).FieldCards)
                {
                    CardRecord record;
                    if(user.Game.GameCardManager.FindCard(fieldCard.CardRecordID, out record) && record is ServantCardRecord)
                    {
                        (record as ServantCardRecord).RemainedHealth -= damage;
                    }
                }
            }
        }
    }
}

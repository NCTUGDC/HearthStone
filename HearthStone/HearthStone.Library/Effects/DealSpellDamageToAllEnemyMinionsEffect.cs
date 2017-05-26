using HearthStone.Library.Effectors;
using HearthStone.Protocol;
using System.Linq;

namespace HearthStone.Library.Effects
{
    public class DealSpellDamageToAllEnemyMinionsEffect : Effect
    {
        public int Damage { get; private set; }
        public DealSpellDamageToAllEnemyMinionsEffect(int effectID, int damage) : base(effectID)
        {
            Damage = damage;
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.DealSpellDamageToAllEnemyMinions;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            if (game != null && game.SelectGamePlayerID(selfGamePlayerID) != -1)
            {
                Field field = game.SelfField(selfGamePlayerID);
                int extraSpellDamage = field.Cards(game.GameCardManager).Sum(x => x.Effectors(game.GameCardManager).OfType<SpellDamageEffector>().Sum(y => y.SpellDamage()));
                if (extraSpellDamage > 0)
                {
                    return $"對全部敵方手下造成*{Damage+ extraSpellDamage}點傷害";
                }
                else
                {
                    return $"對全部敵方手下造成{Damage}點傷害";
                }
            }
            else
            {
                return $"對全部敵方手下造成{Damage}點傷害";
            }
        }
    }
}

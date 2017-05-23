using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class DealDamageToAllEnemyMinionsEffect : Effect
    {
        public int Damage { get; private set; }
        public DealDamageToAllEnemyMinionsEffect(int effectID, int damage) : base(effectID)
        {
            Damage = damage;
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.DealDamageToAllEnemyMinions;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return $"對全部敵方手下造成{Damage}點傷害";
        }
    }
}

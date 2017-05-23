using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class DestroyEnemyMinionEffect : Effect
    {
        public DestroyEnemyMinionEffect(int effectID) : base(effectID)
        {
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.DestroyEnemyMinion;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "摧毀一個敵方手下";
        }
    }
}

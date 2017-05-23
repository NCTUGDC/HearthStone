using HearthStone.Protocol;
using System;

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
                throw new NotImplementedException();
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            throw new NotImplementedException();
        }
    }
}

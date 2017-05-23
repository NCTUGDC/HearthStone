using HearthStone.Protocol;
using System;

namespace HearthStone.Library.Effects
{
    public class DealDamageEffect : Effect
    {
        public int Damage { get; private set; }
        public DealDamageEffect(int effectID, int damage) : base(effectID)
        {
            Damage = damage;
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

using HearthStone.Protocol;
using System;

namespace HearthStone.Library.Effects
{
    public class DoubleMinionAttackEffect : Effect
    {
        public DoubleMinionAttackEffect(int effectID) : base(effectID)
        {
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.DoubleMinionAttack;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "使一個手下的攻擊力加倍";
        }
    }
}

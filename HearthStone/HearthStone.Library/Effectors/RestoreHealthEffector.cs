using HearthStone.Library.CardRecords;
using HearthStone.Library.Effects;

namespace HearthStone.Library.Effectors
{
    public class RestoreHealthEffector : TargetEffector
    {
        public RestoreHealthEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectHero(Hero hero, GamePlayer user)
        {
            Effect effect = Effect;
            if (effect != null && effect is RestoreHealthEffect)
            {
                hero.RemainedHP += (effect as RestoreHealthEffect).RestoreNumber;
            }
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            Effect effect = Effect;
            if (effect != null && effect is RestoreHealthEffect)
            {
                servant.RemainedHealth += (effect as RestoreHealthEffect).RestoreNumber;
            }
        }
    }
}

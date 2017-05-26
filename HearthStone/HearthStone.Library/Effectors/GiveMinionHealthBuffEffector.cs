using HearthStone.Library.CardRecords;
using HearthStone.Library.Effects;

namespace HearthStone.Library.Effectors
{
    public class GiveMinionHealthBuffEffector : MinionTargetEffector
    {
        public GiveMinionHealthBuffEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            Effect effect = Effect;
            if (effect != null && effect is GiveMinionHealthBuffEffect)
            {
                servant.Health += (effect as GiveMinionHealthBuffEffect).BuffNumber;
                servant.RemainedHealth += (effect as GiveMinionHealthBuffEffect).BuffNumber;
            }
        }
    }
}

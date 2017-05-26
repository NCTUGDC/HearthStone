using HearthStone.Library.CardRecords;
using HearthStone.Library.Effects;

namespace HearthStone.Library.Effectors
{
    public class GiveMinionAttackBuffEffector : MinionTargetEffector
    {
        public GiveMinionAttackBuffEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            Effect effect = Effect;
            if (effect != null && effect is GiveMinionAttackBuffEffect)
            {
                servant.Attack += (effect as GiveMinionAttackBuffEffect).BuffNumber;
            }           
        }
    }
}

using HearthStone.Library.CardRecords;
using HearthStone.Library.Cards;

namespace HearthStone.Library.Effectors
{
    public class SilenceMinionEffector : MinionTargetEffector
    {
        public SilenceMinionEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void AffectServant(ServantCardRecord servant, GamePlayer user)
        {
            foreach (int effectorID in servant.EffectorIDs)
            {
                servant.RemoveEffector(effectorID);
            }
            if(servant.Card is ServantCard)
            {
                ServantCard servantCard = servant.Card as ServantCard;
                servant.Attack = servantCard.Attack;
                servant.Health = servantCard.Health;
            }
        }
    }
}

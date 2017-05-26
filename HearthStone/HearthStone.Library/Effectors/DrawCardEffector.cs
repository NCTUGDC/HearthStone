using HearthStone.Library.Effects;

namespace HearthStone.Library.Effectors
{
    public class DrawCardEffector : AutoExecutetEffector
    {
        public DrawCardEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public override void Affect(GamePlayer user)
        {
            Effect effect = Effect;
            if(effect != null && effect is DrawCardEffect)
            {
                user.Draw((effect as DrawCardEffect).CardCount);
            }
        }
    }
}

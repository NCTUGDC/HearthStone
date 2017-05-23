namespace HearthStone.Library.Effectors
{
    public abstract class AutoExecutetEffector : Effector
    {
        public AutoExecutetEffector(int effectorID, int effectID) : base(effectorID, effectID)
        {
        }

        public abstract void Affect(GamePlayer user);
    }
}

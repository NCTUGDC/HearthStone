namespace HearthStone.Library
{
    public abstract class Effector
    {
        public int EffectorID { get; private set; }
        public int EffectID { get; private set; }

        protected Effector(int effectorID, int effectID)
        {
            EffectorID = effectorID;
            EffectID = effectID;
        }
    }
}

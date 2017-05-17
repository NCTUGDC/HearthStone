namespace HearthStone.Library
{
    public class Hero
    {
        public int HeroID { get; private set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public bool IsFrozen { get; set; }


        public Hero() { }
        public Hero(int heroID, int hp, int maxHP, bool isFrozen)
        {
            HeroID = heroID;
            HP = hp;
            MaxHP = maxHP;
            IsFrozen = isFrozen;
        }
    }
}

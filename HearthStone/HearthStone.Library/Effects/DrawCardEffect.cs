using HearthStone.Protocol;

namespace HearthStone.Library.Effects
{
    public class DrawCardEffect : Effect
    {
        public int CardCount { get; private set; }
        public DrawCardEffect(int effectID, int cardCount) : base(effectID)
        {
            CardCount = cardCount;
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.DrawCard;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return $"抽{CardCount}張牌";
        }
    }
}

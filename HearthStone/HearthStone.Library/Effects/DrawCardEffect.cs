using HearthStone.Protocol;
using System;

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
                throw new NotImplementedException();
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            throw new NotImplementedException();
        }
    }
}

using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library.Cards
{
    public class SpellCard : Card
    {
        public override CardTypeCode CardType
        {
            get
            {
                return CardTypeCode.Spell;
            }
        }

        public SpellCard(int cardID, int manaCost, string cardName, List<Effect> effects, RarityCode rarity) : base(cardID, manaCost, cardName, effects, rarity)
        {
            
        }
    }
}

using HearthStone.Protocol;
using System;
using System.Collections.Generic;

namespace HearthStone.Library.Cards
{
    public class WeaponCard : Card
    {
        public override CardTypeCode CardType
        {
            get
            {
                throw new NotImplementedException("WeaponCard CardType");
            }
        }

        public int Attack { get; private set; }
        public int Durability { get; private set; }

        public WeaponCard(int cardID, int manaCost, string cardName, List<Effect> effects, int attack, int durability) : base(cardID, manaCost, cardName, effects)
        {
            throw new NotImplementedException("WeaponCard Constructor");
        }
    }
}

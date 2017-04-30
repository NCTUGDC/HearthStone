using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearthStone.Protocol;

namespace HearthStone.Library.Cards
{
    public class ServantCard : Card
    {
        public override CardTypeCode CardType
        {
            get
            {
                return CardTypeCode.Servant;
            }
        }
        public int Attack { get; private set; }
        public int Health { get; private set; }

        public ServantCard(int cardID, int manaCost, string cardName, List<Effect> effects, int attack, int health) : base(cardID, manaCost, cardName, effects)
        {
            Attack = attack;
            Health = health;
        }
    }
}

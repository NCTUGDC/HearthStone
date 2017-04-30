using HearthStone.Protocol;
using System;
using System.Collections.Generic;
using System.Text;

namespace HearthStone.Library
{
    public abstract class Card
    {
        public int CardID { get; private set; }
        public int ManaCost { get; private set; }
        public string CardName { get; private set; }
        public string Description
        {
            get
            {
                StringBuilder descriptionBuilder = new StringBuilder("");
                effects.ForEach(x => descriptionBuilder.AppendLine(x.Description));
                return descriptionBuilder.ToString();
            }
        }
        public abstract CardTypeCode CardType { get; }
        private List<Effect> effects;

        protected Card(int cardID, int manaCost, string cardName, List<Effect> effects)
        {
            CardID = cardID;
            ManaCost = manaCost;
            CardName = cardName;
            this.effects = effects;
        }
    }
}

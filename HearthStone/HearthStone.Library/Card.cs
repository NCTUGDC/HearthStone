using HearthStone.Protocol;
using System;
using System.Collections.Generic;

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
                throw new NotImplementedException("Card Description");
            }
        }
        public abstract CardTypeCode CardType { get; }
        private List<Effect> effects;

        protected Card(int cardID, int manaCost, string cardName, List<Effect> effects)
        {
            throw new NotImplementedException("Card Constructor");
        }
    }
}

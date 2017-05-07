using HearthStone.Protocol;
using System.Collections.Generic;
using System.Text;

namespace HearthStone.Library
{
    public abstract class Card
    {
        public int CardID { get; private set; }
        public int ManaCost { get; private set; }
        public string CardName { get; private set; }
        public string Description(Game game, int selfGamePlayerID)
        {
            StringBuilder descriptionBuilder = new StringBuilder("");
            for (int i = 0; i < effects.Count; i++)
            {
                descriptionBuilder.Append(effects[i].Description(game, selfGamePlayerID));
                if (i != effects.Count - 1)
                {
                    descriptionBuilder.AppendLine();
                }
            }
            return descriptionBuilder.ToString();
        }
        public abstract CardTypeCode CardType { get; }
        private List<Effect> effects;
        public RarityCode Rarity { get; private set; }

        protected Card(int cardID, int manaCost, string cardName, List<Effect> effects, RarityCode rarity)
        {
            CardID = cardID;
            ManaCost = manaCost;
            CardName = cardName;
            this.effects = effects;
            Rarity = rarity;
        }
        public abstract CardRecord CreateRecord(int cardRecordID);
    }
}

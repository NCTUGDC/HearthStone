using HearthStone.Library.CardRecords;
using HearthStone.Protocol;
using System.Collections.Generic;

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

        public ServantCard() { }
        public ServantCard(int cardID, int manaCost, string cardName, List<Effect> effects, int attack, int health, RarityCode rarity) : base(cardID, manaCost, cardName, effects, rarity)
        {
            Attack = attack;
            Health = health;
        }

        public override CardRecord CreateRecord(int cardRecordID)
        {
            return new ServantCardRecord(cardRecordID, CardID);
        }
    }
}

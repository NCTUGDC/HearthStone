﻿using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library.Cards
{
    public class WeaponCard : Card
    {
        public override CardTypeCode CardType
        {
            get
            {
                return CardTypeCode.Weapon;
            }
        }

        public int Attack { get; private set; }
        public int Durability { get; private set; }

        public WeaponCard() { }
        public WeaponCard(int cardID, int manaCost, string cardName, List<Effect> effects, int attack, int durability, RarityCode rarity) : base(cardID, manaCost, cardName, effects, rarity)
        {
            Attack = attack;
            Durability = durability;
        }
    }
}

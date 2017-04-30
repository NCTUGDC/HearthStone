using System;
using System.Collections.Generic;
using HearthStone.Protocol;
using HearthStone.Library.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    class TestCard : Card
    {
        public override CardTypeCode CardType
        {
            get
            {
                return CardTypeCode.Test;
            }
        }

        public TestCard(int cardID, int manaCost, string cardName, List<Effect> effects) : base(cardID, manaCost, cardName, effects)
        {
        }
    }

    [TestClass]
    public class CardUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Card card = new TestCard(1, 2, "Test", new List<Effect>());

            Assert.IsNotNull(card);
            Assert.Equals(card.CardID, 1);
            Assert.Equals(card.ManaCost, 2);
            Assert.Equals(card.CardName, "Test");
            Assert.Equals(card.Description, "");
        }
        [TestMethod]
        public void ConstructorTestMethod2()
        {
            Card card = new TestCard(1, 2, "Test", new List<Effect> { new TestEffect(1) });

            Assert.IsNotNull(card);
            Assert.Equals(card.CardID, 1);
            Assert.Equals(card.ManaCost, 2);
            Assert.Equals(card.CardName, "Test");
            Assert.Equals(card.Description, "Test Effect");
        }

        [TestMethod]
        public void ServantCardConstructorTestMethod1()
        {
            ServantCard card = new ServantCard(1, 2, "ServantCard", new List<Effect>(), 4, 5);

            Assert.IsNotNull(card);
            Assert.Equals(card.CardID, 1);
            Assert.Equals(card.ManaCost, 2);
            Assert.Equals(card.CardName, "ServantCard");
            Assert.Equals(card.Description, "");
            Assert.Equals(card.Attack, 4);
            Assert.Equals(card.Health, 5);
        }

        [TestMethod]
        public void SpellCardConstructorTestMethod1()
        {
            SpellCard card = new SpellCard(1, 2, "SpellCard", new List<Effect>());

            Assert.IsNotNull(card);
            Assert.Equals(card.CardID, 1);
            Assert.Equals(card.ManaCost, 2);
            Assert.Equals(card.CardName, "SpellCard");
            Assert.Equals(card.Description, "");
        }

        [TestMethod]
        public void WeaponCardConstructorTestMethod1()
        {
            WeaponCard card = new WeaponCard(1, 2, "WeaponCard", new List<Effect>(), 3, 2);

            Assert.IsNotNull(card);
            Assert.Equals(card.CardID, 1);
            Assert.Equals(card.ManaCost, 2);
            Assert.Equals(card.CardName, "WeaponCard");
            Assert.Equals(card.Description, "");
            Assert.Equals(card.Attack, 3);
            Assert.Equals(card.Durability, 2);
        }
    }
}

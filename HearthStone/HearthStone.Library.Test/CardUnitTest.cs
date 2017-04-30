using HearthStone.Library.Cards;
using HearthStone.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "Test");
            Assert.AreEqual(card.Description, "");
            Assert.AreEqual(card.CardType,  CardTypeCode.Test);
        }
        [TestMethod]
        public void ConstructorTestMethod2()
        {
            Card card = new TestCard(1, 2, "Test", new List<Effect> { new TestEffect(1) });

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "Test");
            Assert.AreEqual(card.Description, "Test Effect");
            Assert.AreEqual(card.CardType, CardTypeCode.Test);
        }

        [TestMethod]
        public void ServantCardConstructorTestMethod1()
        {
            ServantCard card = new ServantCard(1, 2, "ServantCard", new List<Effect>(), 4, 5);

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "ServantCard");
            Assert.AreEqual(card.Description, "");
            Assert.AreEqual(card.CardType, CardTypeCode.Servant);
            Assert.AreEqual(card.Attack, 4);
            Assert.AreEqual(card.Health, 5);
        }

        [TestMethod]
        public void SpellCardConstructorTestMethod1()
        {
            SpellCard card = new SpellCard(1, 2, "SpellCard", new List<Effect>());

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "SpellCard");
            Assert.AreEqual(card.Description, "");
            Assert.AreEqual(card.CardType, CardTypeCode.Spell);
        }

        [TestMethod]
        public void WeaponCardConstructorTestMethod1()
        {
            WeaponCard card = new WeaponCard(1, 2, "WeaponCard", new List<Effect>(), 3, 2);

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "WeaponCard");
            Assert.AreEqual(card.Description, "");
            Assert.AreEqual(card.CardType, CardTypeCode.Weapon);
            Assert.AreEqual(card.Attack, 3);
            Assert.AreEqual(card.Durability, 2);
        }
    }
}

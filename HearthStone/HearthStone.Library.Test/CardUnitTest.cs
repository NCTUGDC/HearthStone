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

        public TestCard() { }
        public TestCard(int cardID, int manaCost, string cardName, List<Effect> effects, RarityCode rarity) : base(cardID, manaCost, cardName, effects, rarity)
        {
        }
    }

    [TestClass]
    public class CardUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Card card = new TestCard(1, 2, "Test", new List<Effect>(), RarityCode.Free);

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "Test");
            Assert.AreEqual(card.Description(null, 0), "");
            Assert.AreEqual(card.CardType,  CardTypeCode.Test);
            Assert.AreEqual(card.Rarity, RarityCode.Free);
        }
        [TestMethod]
        public void ConstructorTestMethod2()
        {
            Card card = new TestCard(1, 2, "Test", new List<Effect> { new TestEffect(1) }, RarityCode.Legendary);

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "Test");
            Assert.AreEqual(card.Description(null, 0), "Test Effect");
            Assert.AreEqual(card.CardType, CardTypeCode.Test);
            Assert.AreEqual(card.Rarity, RarityCode.Legendary);
        }
        [TestMethod]
        public void ConstructorTestMethod3()
        {
            Card card = new TestCard();
            Assert.IsNotNull(card);
        }
        [TestMethod]
        public void DescriptionTestMethod1()
        {
            Card card = new TestCard(1, 2, "Test", new List<Effect> { new TestEffect(1), new TestEffect(2) }, RarityCode.Legendary);
            Assert.AreEqual(card.Description(null, 0), "Test Effect\nTest Effect");
        }
    }
}

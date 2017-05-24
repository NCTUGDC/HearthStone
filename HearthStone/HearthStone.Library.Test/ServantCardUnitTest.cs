using HearthStone.Library.Cards;
using HearthStone.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class ServantCardUnitTest
    {
        [TestMethod]
        public void ServantCardConstructorTestMethod1()
        {
            ServantCard card = new ServantCard(1, 2, "ServantCard", new List<Effect>(), 4, 5, RarityCode.Free);

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "ServantCard");
            Assert.AreEqual(card.Description(null, 0), "");
            Assert.AreEqual(card.CardType, CardTypeCode.Servant);
            Assert.AreEqual(card.Attack, 4);
            Assert.AreEqual(card.Health, 5);
            Assert.AreEqual(card.Rarity, RarityCode.Free);
        }
        [TestMethod]
        public void ServantCardConstructorTestMethod2()
        {
            Assert.IsNotNull(new ServantCard());
        }
    }
}

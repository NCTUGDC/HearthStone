using HearthStone.Library.Cards;
using HearthStone.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class SpellCardUnitTest
    {
        [TestMethod]
        public void SpellCardConstructorTestMethod1()
        {
            SpellCard card = new SpellCard(1, 2, "SpellCard", new List<Effect>(), RarityCode.Free);

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "SpellCard");
            Assert.AreEqual(card.Description(null, 0), "");
            Assert.AreEqual(card.CardType, CardTypeCode.Spell);
            Assert.AreEqual(card.Rarity, RarityCode.Free);
        }
        [TestMethod]
        public void SpellCardConstructorTestMethod2()
        {
            Assert.IsNotNull(new SpellCard());
        }
    }
}

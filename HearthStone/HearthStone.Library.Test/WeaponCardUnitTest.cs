using HearthStone.Library.Cards;
using HearthStone.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class WeaponCardUnitTest
    {
        [TestMethod]
        public void WeaponCardConstructorTestMethod1()
        {
            WeaponCard card = new WeaponCard(1, 2, "WeaponCard", new List<Effect>(), 3, 2, RarityCode.Free);

            Assert.IsNotNull(card);
            Assert.AreEqual(card.CardID, 1);
            Assert.AreEqual(card.ManaCost, 2);
            Assert.AreEqual(card.CardName, "WeaponCard");
            Assert.AreEqual(card.Description(null, 0), "");
            Assert.AreEqual(card.CardType, CardTypeCode.Weapon);
            Assert.AreEqual(card.Attack, 3);
            Assert.AreEqual(card.Durability, 2);
            Assert.AreEqual(card.Rarity, RarityCode.Free);
        }
        [TestMethod]
        public void WeaponCardConstructorTestMethod2()
        {
            Assert.IsNotNull(new WeaponCard());
        }
    }
}

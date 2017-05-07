using HearthStone.Library.Cards;
using HearthStone.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

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

        public TestCard(int cardID, int manaCost, string cardName, List<Effect> effects, RarityCode rarity) : base(cardID, manaCost, cardName, effects, rarity)
        {
        }

        public override CardRecord CreateRecord(int cardRecordID)
        {
            throw new NotImplementedException();
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
    }
}

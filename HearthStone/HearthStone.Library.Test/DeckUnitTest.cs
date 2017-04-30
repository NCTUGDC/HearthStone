using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class DeckUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Deck deck = new Deck(1, "Test", 30);

            Assert.IsNotNull(deck);
            Assert.AreEqual(deck.DeckID, 1);
            Assert.AreEqual(deck.DeckName, "Test");
            Assert.AreEqual(deck.MaxCardCount, "");
            Assert.AreEqual(deck.CardCount, 0);
        }
        [TestMethod]
        public void ConstructorTestMethod2()
        {
            Deck deck = new Deck(1, "Test", 30, new List<Card> { new TestCard(0, 0, "Test", new List<Effect>()) });

            Assert.IsNotNull(deck);
            Assert.AreEqual(deck.DeckID, 1);
            Assert.AreEqual(deck.DeckName, "Test");
            Assert.AreEqual(deck.MaxCardCount, "");
            Assert.AreEqual(deck.CardCount, 1);
        }
    }
}

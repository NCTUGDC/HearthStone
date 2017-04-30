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
            Assert.AreEqual(deck.MaxCardCount, 30);
            Assert.AreEqual(deck.CardCount, 0);
        }
        [TestMethod]
        public void ConstructorTestMethod2()
        {
            var card = new TestCard(0, 0, "Test", new List<Effect>());
            Deck deck = new Deck(1, "Test", 30, new List<Card> { card });

            Assert.IsNotNull(deck);
            Assert.AreEqual(deck.DeckID, 1);
            Assert.AreEqual(deck.DeckName, "Test");
            Assert.AreEqual(deck.MaxCardCount, 30);
            Assert.AreEqual(deck.CardCount, 1);
            var cards = deck.Cards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current, card);
            Assert.IsFalse(cards.MoveNext());
        }
    }
}

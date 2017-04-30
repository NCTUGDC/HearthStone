using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            Assert.AreEqual(deck.TotalCardCount, 0);
        }
        [TestMethod]
        public void ConstructorTestMethod2()
        {
            var card = new TestCard(0, 0, "Test", new List<Effect>(), Protocol.RarityCode.Free);
            Deck deck = new Deck(1, "Test", 30, new List<Card> { card });

            Assert.IsNotNull(deck);
            Assert.AreEqual(deck.DeckID, 1);
            Assert.AreEqual(deck.DeckName, "Test");
            Assert.AreEqual(deck.MaxCardCount, 30);
            Assert.AreEqual(deck.TotalCardCount, 1);
            var cards = deck.Cards.GetEnumerator();
            Assert.IsTrue(cards.MoveNext());
            Assert.AreEqual(cards.Current, card);
            Assert.IsFalse(cards.MoveNext());
        }

        [TestMethod]
        public void AddCardTestMethod1()
        {
            var card = new TestCard(0, 0, "Test", new List<Effect>(), Protocol.RarityCode.Free);
            Deck deck = new Deck(1, "Test", 30, new List<Card>());

            Assert.AreEqual(deck.TotalCardCount, 0);
            Assert.IsTrue(deck.AddCard(card));
            Assert.AreEqual(deck.TotalCardCount, 1);
            Assert.IsFalse(deck.AddCard(null));
            Assert.AreEqual(deck.TotalCardCount, 1);
            Assert.IsTrue(deck.AddCard(card));
            Assert.AreEqual(deck.TotalCardCount, 2);
            Assert.IsFalse(deck.AddCard(card));
            Assert.AreEqual(deck.TotalCardCount, 2);
        }
        [TestMethod]
        public void RemoveCardTestMethod1()
        {
            var card = new TestCard(0, 0, "Test", new List<Effect>(), Protocol.RarityCode.Free);
            Deck deck = new Deck(1, "Test", 30, new List<Card>());

            Assert.IsTrue(deck.AddCard(card));
            Assert.IsTrue(deck.AddCard(card));

            Assert.AreEqual(deck.TotalCardCount, 2);
            Assert.IsTrue(deck.RemoveCard(0));
            Assert.AreEqual(deck.TotalCardCount, 1);
            Assert.IsFalse(deck.RemoveCard(-1));
            Assert.AreEqual(deck.TotalCardCount, 1);
            Assert.IsTrue(deck.RemoveCard(0));
            Assert.AreEqual(deck.TotalCardCount, 0);
            Assert.IsFalse(deck.RemoveCard(0));
            Assert.AreEqual(deck.TotalCardCount, 0);
        }
        [TestMethod]
        public void CardCountTestMethod1()
        {
            var card = new TestCard(0, 0, "Test", new List<Effect>(), Protocol.RarityCode.Free);
            Deck deck = new Deck(1, "Test", 30, new List<Card>());

            Assert.AreEqual(deck.CardCount(0), 0);
            Assert.IsTrue(deck.AddCard(card));
            Assert.AreEqual(deck.CardCount(0), 1);
            Assert.IsTrue(deck.AddCard(card));
            Assert.AreEqual(deck.CardCount(0), 2);
            Assert.IsFalse(deck.AddCard(card));
            Assert.AreEqual(deck.CardCount(0), 2);
        }
    }
}

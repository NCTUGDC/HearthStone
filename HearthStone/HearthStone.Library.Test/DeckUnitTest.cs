using System;
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
            Assert.Equals(deck.DeckID, 1);
            Assert.Equals(deck.DeckName, "Test");
            Assert.Equals(deck.MaxCardCount, "");
        }
    }
}

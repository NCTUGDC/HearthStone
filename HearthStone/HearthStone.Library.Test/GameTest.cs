using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Deck deck1 = new Deck(1, "test1", 10), deck2 = new Deck(2, "test2", 10);
            foreach(var card in CardManager.Instance.Cards)
            {
                deck1.AddCard(card);
                deck2.AddCard(card);
            }
            Game game = new Game(1, new Player(1, "test1"), new Player(2, "test2"), deck1, deck2);
            Assert.IsNotNull(game);
        }
    }
}

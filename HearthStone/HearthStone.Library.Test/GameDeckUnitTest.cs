using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class GameDeckUnitTest
    {
        [TestMethod]
        public void DrawTestMethod1()
        {
            GameDeck deck = new GameDeck(1, new List<int> { 1, 2 });
            int cardRecordID;
            Assert.IsTrue(deck.Draw(out cardRecordID));
            Assert.AreEqual(1, cardRecordID);
            Assert.IsTrue(deck.Draw(out cardRecordID));
            Assert.AreEqual(2, cardRecordID);
            Assert.IsFalse(deck.Draw(out cardRecordID));
            Assert.AreEqual(0, cardRecordID);
        }
        [TestMethod]
        public void DrawTestMethod2()
        {
            GameDeck deck = new GameDeck(1, new List<int> { 1, 2 });
            int id = 0;
            deck.OnDrawCard += (eventDeck, eventID) => 
            {
                id = eventID;
            };
            int cardRecordID;
            Assert.IsTrue(deck.Draw(out cardRecordID));
            Assert.AreEqual(1, cardRecordID);
            Assert.AreEqual(1, id);
            Assert.IsTrue(deck.Draw(out cardRecordID));
            Assert.AreEqual(2, cardRecordID);
            Assert.AreEqual(2, id);
            Assert.IsFalse(deck.Draw(out cardRecordID));
            Assert.AreEqual(0, cardRecordID);
            Assert.AreEqual(2, id);
        }
        [TestMethod]
        public void AddCardTestMethod1()
        {
            GameDeck deck = new GameDeck(1, new List<int> { 1, 2 });
            int id = 0;
            deck.OnCardsChanged += (eventDeck, eventID, changeCode) =>
            {
                if(changeCode == Protocol.DataChangeCode.Add)
                    id = eventID;
            };
            deck.AddCard(3);
            Assert.AreEqual(3, id);
        }
        [TestMethod]
        public void RemoveCardTestMethod1()
        {
            GameDeck deck = new GameDeck(1, new List<int> { 1, 2 });
            int id = 0;
            deck.OnCardsChanged += (eventDeck, eventID, changeCode) =>
            {
                if (changeCode == Protocol.DataChangeCode.Remove)
                    id = eventID;
            };
            deck.RemoveCard(2);
            Assert.AreEqual(2, id);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class CardRecordUnitTest
    {
        class TestCardRecord : CardRecord
        {
            public TestCardRecord() : base() { }
            public TestCardRecord(int cardRecordID, int cardID) : base(cardRecordID, cardID) { }

            public void SetCardRecordID(int ID)
            {
                CardRecordID = ID;
            }
        }

        [TestMethod]
        public void CardRecordIDTestMethod1()
        {
            Assert.IsNotNull(CardManager.Instance);
            Assert.IsNotNull(CardManager.Instance.Cards);
            try
            {
                int anyValidCardID = CardManager.Instance.Cards.First<Card>().CardID;

                foreach (int id in new int[] { 0, 1, 2, 3, 4 })
                {
                    TestCardRecord test = new TestCardRecord(id, anyValidCardID);
                    Assert.IsTrue(test.CardRecordID == id, "Invalid Constructor Setter for CardRecordID: " + id);
                }
            }
            catch (ArgumentNullException)
            {
                Assert.Fail("Unable to run the test case, no valid card");
            }
        }

        [TestMethod]
        public void CardRecordIDTestMethod2()
        {
            TestCardRecord test = new TestCardRecord();
            foreach (int id in new int[] { 0, 1, 2, 3, 4 })
            {
                test.SetCardRecordID(id);
                Assert.IsTrue(test.CardRecordID == id, "Invalid Setter for CardRecordID: " + id);
            }
        }

        [TestMethod]
        public void CardIDTestMethod1()
        {
            Assert.IsNotNull(CardManager.Instance);
            Assert.IsNotNull(CardManager.Instance.Cards);
            bool hasCards = false;
            int validCardRecordID = 0; // assume 0 is always valid
            foreach (Card card in CardManager.Instance.Cards)
            {
                hasCards = true;
                TestCardRecord test = new TestCardRecord(validCardRecordID, card.CardID);
                Assert.IsTrue(test.CardID == card.CardID, "Invalid Constructor Setter for CardRecordID: " + card.CardID);
            }
            if (!hasCards)
            {
                Assert.Fail("Unable to run the test case, no valid card");
            }
        }

        [TestMethod]
        public void CardIDTestMethod2()
        {
            Assert.IsNotNull(CardManager.Instance);
            Assert.IsNotNull(CardManager.Instance.Cards);
            int invalidID = int.MinValue; // assume this ID is always invalid
            try
            {
                new TestCardRecord(0, invalidID);
                Assert.Fail("CardRecord should created with an invalid CardID");
            } catch (Exception)
            {
            }
        }

        [TestMethod]
        public void CardTestMethod1()
        {
            Assert.IsNotNull(CardManager.Instance);
            Assert.IsNotNull(CardManager.Instance.Cards);
            bool hasCards = false;
            int validCardRecordID = 0; // assume 0 is always valid
            foreach (Card card in CardManager.Instance.Cards)
            {
                hasCards = true;
                TestCardRecord test = new TestCardRecord(validCardRecordID, card.CardID);
                Assert.IsTrue(test.Card == card, "Invalid Card getter for " + card.CardID);
            }
            if (!hasCards)
            {
                Assert.Fail("Unable to run the test case, no valid card");
            }
        }

        [TestMethod]
        public void CardTestMethod2()
        {
            TestCardRecord test = new TestCardRecord();
            Assert.IsNull(test.Card, "Card getter return a non-null object for empty CardRecord");
        }

        [TestMethod]
        public void EffectorIDsTestMethod1()
        {
            TestCardRecord test = new TestCardRecord();
            Assert.IsNotNull(test.EffectorIDs);
        }

        [TestMethod]
        public void ManaCostTestMethod1()
        {
            TestCardRecord test = new TestCardRecord();
            for (int i = 0; i < 100; i++)
            {
                test.ManaCost = i;
                Assert.IsTrue(test.ManaCost == i, "Invalid ManaCost for " + i);
            }
        }

        [TestMethod]
        public void ManaCostTestMethod2()
        {
            TestCardRecord test = new TestCardRecord();
            for (int i = 0; i > -100; i--)
            {
                test.ManaCost = i;
                Assert.IsTrue(test.ManaCost == 0, "ManaCost should always >= 0 (" + i + ")");
            }
        }
    }
}

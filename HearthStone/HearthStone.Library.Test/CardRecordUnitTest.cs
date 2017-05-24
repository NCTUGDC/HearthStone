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

        // assume the following IDs are always valid/invalid for testing
        public const int assumeValidCardID = 1;
        public const int assumeValidCardRecordID = 1;
        public const int assumeInvalidCardID = int.MinValue;
        public const int assumeInvalidCardRecordID = int.MinValue;

        [TestMethod]
        public void CardRecordIDTestMethod1()
        {
            Assert.IsNotNull(CardManager.Instance);
            Assert.IsNotNull(CardManager.Instance.Cards);

            if (CardManager.Instance.Cards.Count<Card>() > 0)
            {
                foreach (int id in new int[] { 0, 1, 2, 3, 4 })
                {
                    TestCardRecord test = new TestCardRecord(id, assumeValidCardID);
                    Assert.IsTrue(test.CardRecordID == id, "Invalid Constructor Setter for CardRecordID: " + id);
                }
            }
            else
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
            if (CardManager.Instance.Cards.Count() == 0)
            {
                Assert.Fail("Unable to run the test case, no valid card");
            }
            foreach (Card card in CardManager.Instance.Cards)
            {
                TestCardRecord test = new TestCardRecord(assumeValidCardRecordID, card.CardID);
                Assert.IsTrue(test.CardID == card.CardID, "Invalid Constructor Setter for CardRecordID: " + card.CardID);
            }
        }

        [TestMethod]
        public void CardIDTestMethod2()
        {
            Assert.IsNotNull(CardManager.Instance);
            Assert.IsNotNull(CardManager.Instance.Cards);
            try
            {
                new TestCardRecord(0, assumeInvalidCardID);
                Assert.Fail("CardRecord should created with an invalid CardID");
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void CardTestMethod1()
        {
            Assert.IsNotNull(CardManager.Instance);
            Assert.IsNotNull(CardManager.Instance.Cards);
            if (CardManager.Instance.Cards.Count() == 0)
            {
                Assert.Fail("Unable to run the test case, no valid card");
            }
            foreach (Card card in CardManager.Instance.Cards)
            {
                TestCardRecord test = new TestCardRecord(assumeValidCardRecordID, card.CardID);
                Assert.IsTrue(test.Card == card, "Invalid Card getter for " + card.CardID);
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
        public void EffectorIDsTestMethod2()
        {
            TestCardRecord test = new TestCardRecord();
            Assert.IsTrue(test.EffectorIDs.Count() == 0, "Empty CardRecord should not have any elements in EffectorIDs");
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

        [TestMethod]
        public void CardRecordConstructorTestMethod1()
        {
            try
            {
                new TestCardRecord();

                Assert.IsNotNull(CardManager.Instance);
                Assert.IsNotNull(CardManager.Instance.Cards);
                if (CardManager.Instance.Cards.Count() == 0)
                {
                    Assert.Fail("Unable to run the test case, no valid card");
                }

                foreach (Card card in CardManager.Instance.Cards)
                {
                    new TestCardRecord(assumeValidCardRecordID, card.CardID);
                }
            }
            catch (Exception)
            {
                Assert.Fail("Unable to construct valid CardRecord");
            }
        }

        [TestMethod]
        public void CardRecordConstructorTestMethod2()
        {
            try
            {
                new TestCardRecord(assumeInvalidCardRecordID, assumeInvalidCardID);
                Assert.Fail("An invalid CardRecord should not be created");
            }
            catch (Exception)
            {
            }
        }

        [TestMethod]
        public void AddEffectorTestMethod1()
        {
            int[][] patterns = new int[][]
            {
                new int[] {  },
                new int[] { 1 },
                new int[] { 2 },
                new int[] { 1, 2 },
                new int[] { 2, 1 },
                new int[] { 1, 2, 3 },
                new int[] { 1, 3, 2 },
                new int[] { 2, 1, 3 },
                new int[] { 2, 3, 1 },
                new int[] { 3, 1, 2 },
                new int[] { 3, 2, 1 },
            };
            foreach (int[] pattern in patterns)
            {
                TestCardRecord test = new TestCardRecord();
                foreach (int effectorID in pattern)
                {
                    bool isAdded = test.AddEffector(effectorID);
                    Assert.IsTrue(isAdded, "Invalid AddEffector (return false for a valid add");
                }
                foreach (int effectorID in pattern)
                {
                    bool isAdded = test.AddEffector(effectorID);
                    Assert.IsFalse(isAdded, "Invalid AddEffector (return true for an invalid add");
                }
            }
        }

        [TestMethod]
        public void AddEffectorTestMethod2()
        {
            int[][] patterns = new int[][]
            {
                new int[] {  },
                new int[] { 1 },
                new int[] { 2 },
                new int[] { 1, 2 },
                new int[] { 2, 1 },
                new int[] { 1, 2, 3 },
                new int[] { 1, 3, 2 },
                new int[] { 2, 1, 3 },
                new int[] { 2, 3, 1 },
                new int[] { 3, 1, 2 },
                new int[] { 3, 2, 1 },
            };
            foreach (int[] pattern in patterns)
            {
                TestCardRecord test = new TestCardRecord();
                foreach (int effectorID in pattern)
                {
                    test.AddEffector(effectorID);
                }
                Assert.IsTrue(test.EffectorIDs.Count() == pattern.Length, "Invalid AddEffector (size mismatch)");
                foreach (int effectorID in pattern)
                {
                    test.AddEffector(effectorID);
                }
                Assert.IsTrue(test.EffectorIDs.Count() == pattern.Length, "Invalid AddEffector (size mismatch)");
            }
        }

        [TestMethod]
        public void AddEffectorTestMethod3()
        {
            TestCardRecord test = new TestCardRecord();
            for (int i = 1; i <= 100; i++)
            {
                Assert.IsTrue(test.AddEffector(i), "Invalid AddEffector (return false for a valid add");
                Assert.IsTrue(test.EffectorIDs.Count() == i, "Invalid AddEffector (size mismatch)");
            }
        }

        [TestMethod]
        public void RemoveEffectorTestMethod1()
        {
            int[][] patterns = new int[][]
            {
                new int[] {  },
                new int[] { 1 },
                new int[] { 2 },
                new int[] { 1, 2 },
                new int[] { 2, 1 },
                new int[] { 1, 2, 3 },
                new int[] { 1, 3, 2 },
                new int[] { 2, 1, 3 },
                new int[] { 2, 3, 1 },
                new int[] { 3, 1, 2 },
                new int[] { 3, 2, 1 },
            };
            foreach (int[] pattern in patterns)
            {
                TestCardRecord test = new TestCardRecord();
                foreach (int effectorID in pattern)
                {
                    test.AddEffector(effectorID);
                }
                foreach (int effectorID in pattern)
                {
                    bool isRemoved = test.RemoveEffector(effectorID);
                    Assert.IsTrue(isRemoved, "Invalid RemoveEffector (return false for a valid remove");
                }
                foreach (int effectorID in pattern)
                {
                    bool isRemoved = test.RemoveEffector(effectorID);
                    Assert.IsFalse(isRemoved, "Invalid AddEffector (return true for an invalid remove");
                }
            }
        }

        [TestMethod]
        public void RemoveEffectorTestMethod2()
        {
            int[][] patterns = new int[][]
            {
                new int[] {  },
                new int[] { 1 },
                new int[] { 2 },
                new int[] { 1, 2 },
                new int[] { 2, 1 },
                new int[] { 1, 2, 3 },
                new int[] { 1, 3, 2 },
                new int[] { 2, 1, 3 },
                new int[] { 2, 3, 1 },
                new int[] { 3, 1, 2 },
                new int[] { 3, 2, 1 },
            };
            foreach (int[] pattern in patterns)
            {
                TestCardRecord test = new TestCardRecord();
                foreach (int effectorID in pattern)
                {
                    test.AddEffector(effectorID);
                }
                foreach (int effectorID in pattern)
                {
                    bool isRemoved = test.RemoveEffector(effectorID);
                    Assert.IsTrue(isRemoved, "Invalid RemoveEffector (return false for a valid remove");
                }
                Assert.IsTrue(test.EffectorIDs.Count() == 0, "Invalid RemoveEffector (size mismatch)");
                foreach (int effectorID in pattern)
                {
                    bool isRemoved = test.RemoveEffector(effectorID);
                    Assert.IsFalse(isRemoved, "Invalid RemoveEffector (return true for an invalid remove");
                }
                Assert.IsTrue(test.EffectorIDs.Count() == 0, "Invalid RemoveEffector (size mismatch)");
            }
        }

        [TestMethod]
        public void RemoveEffectorTestMethod3()
        {
            TestCardRecord test = new TestCardRecord();
            for (int i = 1; i <= 100; i++)
            {
                Assert.IsTrue(test.AddEffector(i), "Invalid AddEffector (return false for a valid add");
                Assert.IsTrue(test.RemoveEffector(i), "Invalid RemoveEffector (return false for a valid remove");
                Assert.IsFalse(test.RemoveEffector(i), "Invalid RemoveEffector (return true for an invalid remove");
                Assert.IsTrue(test.EffectorIDs.Count() == 0, "Invalid RemoveEffector (size mismatch)");
            }
        }

        [TestMethod]
        public void DestroyTestMethod1()
        {
            try
            {
                TestCardRecord test = new TestCardRecord();
                test.Destroy();
            }
            catch (Exception)
            {
                Assert.Fail("Failed destory");
            }
        }

        [TestMethod]
        public void DestroyTestMethod2()
        {
            try
            {
                Assert.IsNotNull(CardManager.Instance);
                Assert.IsNotNull(CardManager.Instance.Cards);
                if (CardManager.Instance.Cards.Count() == 0)
                {
                    Assert.Fail("Unable to run the test case, no valid card");
                }

                foreach (Card card in CardManager.Instance.Cards)
                {
                    TestCardRecord test = new TestCardRecord(assumeValidCardRecordID, card.CardID);
                    test.Destroy();
                }
            }
            catch (Exception)
            {
                Assert.Fail("Failed destory");
            }
        }


        [TestMethod]
        public void OnManaCostChangedTestMethod1()
        {
            int manaCostChangedCount = 0;
            TestCardRecord test = new TestCardRecord();
            test.OnManaCostChanged += (CardRecord card) =>
            {
                manaCostChangedCount++;
            };
            test.ManaCost = 1;
            Assert.IsTrue(manaCostChangedCount == 1, "OnManaCostChanged is not invoked when ManaCost changed");
            test.ManaCost = 0;
            Assert.IsTrue(manaCostChangedCount == 2, "OnManaCostChanged is not invoked when ManaCost changed");
        }
    }
}

using HearthStone.Library.CardRecords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class ServantCardRecordUnitTest
    {
        [TestMethod]
        public void ServantCardRecordConstructorTestMethod1()
        {
            //test default constructor
            ServantCardRecord oCardRecord = new ServantCardRecord();
            Assert.IsNotNull(oCardRecord);
        }

        [TestMethod]
        public void ServantCardRecordConstructorTestMethod2()
        {
            //test legal ServantCardRecord constructor with legal cardID
            ServantCardRecord oCardRecord = new ServantCardRecord(1, 2);
            Assert.IsNotNull(oCardRecord);
            Assert.AreEqual(oCardRecord.CardRecordID, 1);
            Assert.AreEqual(oCardRecord.CardID, 2);
        }

        [TestMethod]
        public void ServantCardRecordConstructorTestMethod3()
        {
            //test legal ServantCardRecord constructor with illegal cardID
            ServantCardRecord oCardRecord = new ServantCardRecord(1, 12);
            Assert.IsNotNull(oCardRecord);
            Assert.AreEqual(oCardRecord.CardRecordID, -1);
        }

        [TestMethod]
        public void ServantCardRecordAttackTestMethod()
        {
            ServantCardRecord oCardRecord = new ServantCardRecord(1, 2);
            //attack
            oCardRecord.OnAttackChanged += (eventAttack) =>
            {
                //do nothing
            };
            oCardRecord.Attack = 3;
            Assert.AreEqual(oCardRecord.Attack, 3);
            oCardRecord.Attack = -3;
            Assert.AreEqual(oCardRecord.Attack, 0);
        }

        [TestMethod]
        public void ServantCardRecordHealthTestMethod()
        {
            ServantCardRecord oCardRecord = new ServantCardRecord(1, 2);
            //health
            oCardRecord.OnHealthChanged += (eventHealth) =>
            {
                //do nothing
            };
            oCardRecord.Health = 5;
            Assert.AreEqual(oCardRecord.Health, 5);
            oCardRecord.Health = -5;
            Assert.AreEqual(oCardRecord.Health, 0);
        }

        [TestMethod]
        public void ServantCardRecordRemainedHealthTestMethod()
        {
            ServantCardRecord oCardRecord = new ServantCardRecord(1, 2);
            //remain health
            oCardRecord.OnRemainedHealthChanged += (eventRemainedHealth) =>
            {
                //do nothing
            };
            oCardRecord.Health = 5;
            oCardRecord.RemainedHealth = 4;
            Assert.AreEqual(oCardRecord.RemainedHealth, 4);
            oCardRecord.RemainedHealth = 6;
            Assert.AreEqual(oCardRecord.RemainedHealth, 5);
        }

        [TestMethod]
        public void ServantCardRecordIsDisplayInThisTurnChangedTestMethod()
        {
            ServantCardRecord oCardRecord = new ServantCardRecord(1, 2);
            //is display in this turn
            oCardRecord.OnIsDisplayInThisTurnChanged += (eventIsDisplayInThisTurnChanged) =>
            {
                //do nothing
            };
            oCardRecord.IsDisplayInThisTurn = false;
            Assert.AreEqual(oCardRecord.IsDisplayInThisTurn, false);
        }

        [TestMethod]
        public void ServantCardRecordAttackCountInThisTurnChangedTestMethod()
        {
            ServantCardRecord oCardRecord = new ServantCardRecord(1, 2);
            //attack count in this turn
            oCardRecord.AttackCountInThisTurn = 7;
            Assert.AreEqual(oCardRecord.AttackCountInThisTurn, 7);
            oCardRecord.OnAttackCountInThisTurnChanged += (eventAttackCountInThisTurnChanged) =>
            {
                //do nothing
            };
            oCardRecord.AttackCountInThisTurn = 7;
            Assert.AreEqual(oCardRecord.AttackCountInThisTurn, 7);
        }
    }
}

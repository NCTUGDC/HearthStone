using HearthStone.Library.CardRecords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class ServantCardRecordUnitTest
    {
        [TestMethod]
        public void ServantCardRecordConstructorTestMethod()
        {
            ServantCardRecord oCardRecord = new ServantCardRecord();
            oCardRecord = new ServantCardRecord(1, 2);
            Assert.AreEqual(oCardRecord.CardRecordID, 1);
            Assert.AreEqual(oCardRecord.CardID, 2);
        }

        public void ServantCardRecordVariableAndEventTestMethod()
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

            //health
            oCardRecord.OnHealthChanged += (eventHealth) =>
            {
                //do nothing
            };
            oCardRecord.Health = 5;
            Assert.AreEqual(oCardRecord.Health, 5);
            oCardRecord.Health = -5;
            Assert.AreEqual(oCardRecord.Health, 0);

            //remain health
            oCardRecord.OnRemainedHealthChanged += (eventRemainedHealth) =>
            {
                //do nothing
            };
            oCardRecord.RemainedHealth = 4;
            Assert.AreEqual(oCardRecord.RemainedHealth, 4);
            oCardRecord.RemainedHealth = 6;
            Assert.AreEqual(oCardRecord.RemainedHealth, 5);

            //is display in this turn
            oCardRecord.OnIsDisplayInThisTurnChanged += (eventIsDisplayInThisTurnChanged) =>
            {
                //do nothing
            };
            oCardRecord.IsDisplayInThisTurn = false;
            Assert.AreEqual(oCardRecord.IsDisplayInThisTurn, false);

            //attack count in this turn
            oCardRecord.OnAttackCountInThisTurnChanged += (eventAttackCountInThisTurnChanged) =>
            {
                //do nothing
            };
            oCardRecord.AttackCountInThisTurn = 7;
            Assert.AreEqual(oCardRecord.AttackCountInThisTurn, 7);

        }
    }
}

using HearthStone.Library.CardRecords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    [TestClass]
    class ServantCardRecordUnitTest
    {
        [TestMethod]
        void ServantCardRecordConstructorTestMethod()
        {
            ServantCardRecord oCardRecord = new ServantCardRecord();
            oCardRecord = new ServantCardRecord(1, 2);
            Assert.Equals(oCardRecord.CardRecordID, 1);
            Assert.Equals(oCardRecord.CardID, 2);
        }

        void ServantCardRecordVariableAndEventTestMethod()
        {
            ServantCardRecord oCardRecord = new ServantCardRecord(1, 2);
            //attack
            oCardRecord.OnAttackChanged += (eventAttack) =>
            {
                //do nothing
            };
            oCardRecord.Attack = 3;
            Assert.Equals(oCardRecord.Attack, 3);
            oCardRecord.Attack = -3;
            Assert.Equals(oCardRecord.Attack, 0);

            //health
            oCardRecord.OnHealthChanged += (eventHealth) =>
            {
                //do nothing
            };
            oCardRecord.Health = 5;
            Assert.Equals(oCardRecord.Health, 5);
            oCardRecord.Health = -5;
            Assert.Equals(oCardRecord.Health, 0);

            //remain health
            oCardRecord.OnRemainedHealthChanged += (eventRemainedHealth) =>
            {
                //do nothing
            };
            oCardRecord.RemainedHealth = 4;
            Assert.Equals(oCardRecord.RemainedHealth, 4);
            oCardRecord.RemainedHealth = 6;
            Assert.Equals(oCardRecord.RemainedHealth, 5);

            //is display in this turn
            oCardRecord.OnIsDisplayInThisTurnChanged += (eventIsDisplayInThisTurnChanged) =>
            {
                //do nothing
            };
            oCardRecord.IsDisplayInThisTurn = false;
            Assert.Equals(oCardRecord.IsDisplayInThisTurn, false);

            //attack count in this turn
            oCardRecord.OnAttackCountInThisTurnChanged += (eventAttackCountInThisTurnChanged) =>
            {
                //do nothing
            };
            oCardRecord.AttackCountInThisTurn = 7;
            Assert.Equals(oCardRecord.AttackCountInThisTurn, 7);

        }
    }
}

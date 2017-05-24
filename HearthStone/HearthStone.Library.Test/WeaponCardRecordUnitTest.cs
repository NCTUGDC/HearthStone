using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class WeaponCardRecordUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord();

            Assert.IsNotNull(weaponcardrecord);
        }
        [TestMethod]
        public void ConstructorTestMethod2()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord(0, 13);

            Assert.IsNotNull(weaponcardrecord);
        }
        
        [TestMethod]
        public void ConstructorTestMethod3()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord(0, 5);

            Assert.IsNotNull(weaponcardrecord);
            Assert.AreEqual(weaponcardrecord.CardRecordID, -1);
        }
        

        [TestMethod]
        public void DurabilityTestMethod1()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord(0, 13);

            Assert.IsNotNull(weaponcardrecord);
            Assert.AreEqual(weaponcardrecord.Durability, 8);

            weaponcardrecord.Durability = 5;
            Assert.AreEqual(weaponcardrecord.Durability, 5);
        }

        [TestMethod]
        public void DurabilityTestMethod2()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord(0, 13);

            int eventCallCounter = 0;
            weaponcardrecord.OnDurabilityChanged += (chageCode) =>
            {
                eventCallCounter++;
            };

            Assert.IsNotNull(weaponcardrecord);
            weaponcardrecord.Durability = 5;
            Assert.AreEqual(weaponcardrecord.Durability, 5);
            Assert.AreEqual(eventCallCounter, 1);
        }

        [TestMethod]
        public void RemainedDurabilityTestMethod1()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord(0, 13);

            Assert.IsNotNull(weaponcardrecord);
            Assert.AreEqual(weaponcardrecord.RemainedDurability, 8);

            weaponcardrecord.RemainedDurability = 5;
            Assert.AreEqual(weaponcardrecord.RemainedDurability, 5);
        }

        [TestMethod]
        public void RemainedDurabilityTestMethod2()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord(0, 13);

            int eventCallCounter = 0;
            weaponcardrecord.OnRemainedDurabilityChanged += (chageCode) =>
            {
                eventCallCounter++;
            };

            Assert.IsNotNull(weaponcardrecord);
            weaponcardrecord.RemainedDurability = 5;
            Assert.AreEqual(weaponcardrecord.RemainedDurability, 5);
            Assert.AreEqual(eventCallCounter, 1);
        }

        [TestMethod]
        public void AttackTestMethod1()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord(0, 13);

            Assert.IsNotNull(weaponcardrecord);
            Assert.AreEqual(weaponcardrecord.Attack, 2);

            weaponcardrecord.Attack = 5;
            Assert.AreEqual(weaponcardrecord.Attack, 5);
        }

        [TestMethod]
        public void AttackTestMethod2()
        {
            CardRecords.WeaponCardRecord weaponcardrecord = new CardRecords.WeaponCardRecord(0, 13);

            int eventCallCounter = 0;
            weaponcardrecord.OnAttackChanged += (chageCode) =>
            {
                eventCallCounter++;
            };

            Assert.IsNotNull(weaponcardrecord);
            weaponcardrecord.Attack = 5;
            Assert.AreEqual(weaponcardrecord.Attack, 5);
            Assert.AreEqual(eventCallCounter, 1);
        }

    }
}

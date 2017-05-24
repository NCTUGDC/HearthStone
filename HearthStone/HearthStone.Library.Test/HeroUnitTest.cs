using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class HeroUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Assert.IsNotNull(new Hero());
            Hero hero = new Hero(1, 15, 30);
            Assert.IsNotNull(hero);
            Assert.AreEqual(1, hero.HeroID);
            Assert.AreEqual(15, hero.RemainedHP);
            Assert.AreEqual(30, hero.HP);
            Assert.IsNotNull(hero.EffectorIDs);
            Assert.AreEqual(0, hero.AttackCountInThisTurn);
        }
        [TestMethod]
        public void WeaponCardRecordIDTestMethod1()
        {
            Hero hero = new Hero(1, 15, 30);
            int addCount = 0, removeCount = 0;
            hero.OnWeaponChanged += (eventHero, changeCode) => 
            {
                if (changeCode == Protocol.DataChangeCode.Add)
                    addCount++;
                if (changeCode == Protocol.DataChangeCode.Remove)
                    removeCount++;
            };
            hero.WeaponCardRecordID = 1;
            Assert.AreEqual(1, hero.WeaponCardRecordID);
            Assert.AreEqual(1, addCount);
            Assert.AreEqual(0, removeCount);

            hero.WeaponCardRecordID = 2;
            Assert.AreEqual(2, hero.WeaponCardRecordID);
            Assert.AreEqual(2, addCount);
            Assert.AreEqual(1, removeCount);
        }
        [TestMethod]
        public void WeaponCardRecordIDTestMethod2()
        {
            Hero hero = new Hero(1, 15, 30);
            hero.WeaponCardRecordID = 1;
            Assert.AreEqual(1, hero.WeaponCardRecordID);

            hero.WeaponCardRecordID = 2;
            Assert.AreEqual(2, hero.WeaponCardRecordID);
        }
        [TestMethod]
        public void AttackTestMethod1()
        {
            Hero hero = new Hero(1, 15, 30);
            Assert.AreEqual(0, hero.Attack);
            hero.Attack = 1;
            Assert.AreEqual(1, hero.Attack);

            int delta = 0;
            hero.OnAttackChanged += (eventHero, eventDelta) => 
            {
                delta = eventDelta;
            };
            hero.Attack = 0;
            Assert.AreEqual(0, hero.Attack);
            Assert.AreEqual(-1, delta);

            hero.Attack = -1;
            Assert.AreEqual(0, hero.Attack);
            Assert.AreEqual(0, delta);

            hero.Attack = 5;
            Assert.AreEqual(5, hero.Attack);
            Assert.AreEqual(5, delta);
        }
        [TestMethod]
        public void HP_TestMethod1()
        {
            Hero hero = new Hero(1, 15, 30);
            hero.HP = 10;
            Assert.AreEqual(10, hero.RemainedHP);
            Assert.AreEqual(10, hero.HP);

            hero.HP = 30;
            Assert.AreEqual(10, hero.RemainedHP);
            Assert.AreEqual(30, hero.HP);

            int delta1 = 0, delta2 = 0;
            hero.OnRemainedHP_Changed += (eventHero, eventDelta) =>
            {
                delta1 = eventDelta;
            };
            hero.OnHP_Changed += (eventHero, eventDelta) =>
            {
                delta2 = eventDelta;
            };
            hero.RemainedHP = 60;
            Assert.AreEqual(30, hero.RemainedHP);
            Assert.AreEqual(30, hero.HP);
            Assert.AreEqual(20, delta1);

            hero.HP = 20;
            Assert.AreEqual(20, hero.RemainedHP);
            Assert.AreEqual(20, hero.HP);
            Assert.AreEqual(-10, delta1);
            Assert.AreEqual(-10, delta2);
        }
        [TestMethod]
        public void AttackCountInThisTurnTestMethod1()
        {
            Hero hero = new Hero(1, 15, 30);
            hero.AttackCountInThisTurn = 1;
            Assert.AreEqual(1, hero.AttackCountInThisTurn);
            int counter = 0;
            hero.OnAttackCountInThisTurnChanged += (eventHero) => 
            {
                counter++;
            };
            hero.AttackCountInThisTurn = 2;
            Assert.AreEqual(2, hero.AttackCountInThisTurn);
            Assert.AreEqual(1, counter);
        }
        [TestMethod]
        public void AddAndRemoveEffectorTestMethod1()
        {
            Hero hero = new Hero(1, 15, 30);
            Assert.IsTrue(hero.AddEffector(1));
            Assert.IsFalse(hero.AddEffector(1));
            Assert.AreEqual(1, hero.EffectorIDs.Count(x => x == 1));

            Assert.IsTrue(hero.RemoveEffector(1));
            Assert.IsFalse(hero.RemoveEffector(1));
            Assert.AreEqual(0, hero.EffectorIDs.Count(x => x == 1));

            int effectorID = 0, addCounter = 0, removeCounter = 0;
            hero.OnEffectorChanged += (eventHero, eventEffectorID, changeCode) => 
            {
                effectorID = eventEffectorID;
                if (changeCode == Protocol.DataChangeCode.Add)
                    addCounter++;
                else if (changeCode == Protocol.DataChangeCode.Remove)
                    removeCounter++;
            };
            Assert.IsTrue(hero.AddEffector(1));
            Assert.IsFalse(hero.AddEffector(1));
            Assert.AreEqual(1, hero.EffectorIDs.Count(x => x == 1));
            Assert.AreEqual(1, effectorID);
            Assert.AreEqual(1, addCounter);
            Assert.AreEqual(0, removeCounter);

            Assert.IsTrue(hero.RemoveEffector(1));
            Assert.IsFalse(hero.RemoveEffector(1));
            Assert.AreEqual(0, hero.EffectorIDs.Count(x => x == 1));
            Assert.AreEqual(1, effectorID);
            Assert.AreEqual(1, addCounter);
            Assert.AreEqual(1, removeCounter);
        }
        [TestMethod]
        public void AttackWithWeaponTestMethod1()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Hero hero = game.GamePlayer1.Hero;
            game.CurrentGamePlayerID = 1;
            new Library.CardRecords.WeaponCardRecord(1, 1);
            Assert.Fail();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using HearthStone.Library.CardRecords;

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
        public void AttackWithWeaponTestMethod1()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Hero hero = game.GamePlayer1.Hero;
            Assert.AreEqual(0, hero.AttackWithWeapon(game));
            game.CurrentGamePlayerID = 1;
            Card card;
            CardManager.Instance.FindCard(12, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            hero.WeaponCardRecordID = record.CardRecordID;
            Assert.AreEqual(2, hero.AttackWithWeapon(game));
            game.CurrentGamePlayerID = 2;
            Assert.AreEqual(0, hero.AttackWithWeapon(game));
        }
        [TestMethod]
        public void AttackHeroTestMethod1()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Hero hero = game.GamePlayer1.Hero;
            Assert.IsFalse(hero.AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
        }
        [TestMethod]
        public void AttackHeroTestMethod2()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Hero hero = game.GamePlayer1.Hero;
            hero.Attack = 2;
            game.GamePlayer2.Hero.Attack = 1;
            Assert.IsTrue(hero.AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
            Assert.AreEqual(29, hero.RemainedHP);
            Assert.AreEqual(28, game.GamePlayer2.Hero.RemainedHP);
            Assert.AreEqual(1, hero.AttackCountInThisTurn);
            Assert.AreEqual(0, game.GamePlayer2.Hero.AttackCountInThisTurn);
        }
        [TestMethod]
        public void AttackHeroTestMethod3()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Hero hero = game.GamePlayer1.Hero;
            hero.Attack = 1;
            Card card;
            CardManager.Instance.FindCard(8, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field2.AddCard(record.CardRecordID, 0));
            Assert.IsTrue(game.Field2.AnyTauntServant());
            Assert.IsFalse(hero.AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
            Assert.AreEqual(0, hero.AttackCountInThisTurn);
            Assert.AreEqual(30, game.GamePlayer2.Hero.RemainedHP);
        }
        [TestMethod]
        public void AttackHeroTestMethod4()
        {
            Game game = GameUnitTest.InitialGameStatus();
            game.CurrentGamePlayerID = 1;
            Hero hero = game.GamePlayer1.Hero;
            Card card;
            CardManager.Instance.FindCard(13, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            hero.WeaponCardRecordID = record.CardRecordID;
            Assert.IsTrue(hero.AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
            Assert.AreEqual(28, game.GamePlayer2.Hero.RemainedHP);
            Assert.AreEqual(1, hero.AttackCountInThisTurn);
            Assert.AreEqual(7, (record as WeaponCardRecord).Durability);

            Assert.IsTrue(hero.AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
            Assert.AreEqual(26, game.GamePlayer2.Hero.RemainedHP);
            Assert.AreEqual(2, hero.AttackCountInThisTurn);
            Assert.AreEqual(6, (record as WeaponCardRecord).Durability);

            Assert.IsFalse(hero.AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
            Assert.AreEqual(26, game.GamePlayer2.Hero.RemainedHP);
            Assert.AreEqual(2, hero.AttackCountInThisTurn);
            Assert.AreEqual(6, (record as WeaponCardRecord).Durability);
        }
        [TestMethod]
        public void AttackServantTestMethod1()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Hero hero = game.GamePlayer1.Hero;
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            game.Field2.AddCard(record.CardRecordID, 0);
            Assert.IsFalse(hero.AttackServant(record as ServantCardRecord, game.GamePlayer1));
        }
        [TestMethod]
        public void AttackServantTestMethod2()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Hero hero = game.GamePlayer1.Hero;
            hero.Attack = 3;
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            game.Field2.AddCard(record.CardRecordID, 0);
            Assert.IsTrue(hero.AttackServant(record as ServantCardRecord, game.GamePlayer1));
            Assert.AreEqual(29, hero.RemainedHP);
            Assert.AreEqual(-1, (record as ServantCardRecord).RemainedHealth);
            Assert.AreEqual(1, hero.AttackCountInThisTurn);
            Assert.AreEqual(0, (record as ServantCardRecord).AttackCountInThisTurn);
        }
        [TestMethod]
        public void AttackServantTestMethod3()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Hero hero = game.GamePlayer1.Hero;
            hero.Attack = 1;
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord servant1Record = game.GameCardManager.CreateCardRecord(card);
            game.Field2.AddCard(servant1Record.CardRecordID, 0);
            CardManager.Instance.FindCard(8, out card);
            CardRecord servant2Record = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field2.AddCard(servant2Record.CardRecordID, 0));
            Assert.IsTrue(game.Field2.AnyTauntServant());
            Assert.IsFalse(hero.AttackServant((servant1Record as ServantCardRecord), game.GamePlayer1));
            Assert.AreEqual(0, hero.AttackCountInThisTurn);
            Assert.AreEqual(30, game.GamePlayer2.Hero.RemainedHP);
            Assert.IsTrue(hero.AttackServant((servant2Record as ServantCardRecord), game.GamePlayer1));
            Assert.AreEqual(1, hero.AttackCountInThisTurn);
            Assert.AreEqual(29, hero.RemainedHP);
            Assert.AreEqual(1, (servant2Record as ServantCardRecord).RemainedHealth);
        }
        [TestMethod]
        public void AttackServantTestMethod4()
        {
            Game game = GameUnitTest.InitialGameStatus();
            game.CurrentGamePlayerID = 1;
            Hero hero = game.GamePlayer1.Hero;
            Card card;
            CardManager.Instance.FindCard(13, out card);
            CardRecord weaponRecord = game.GameCardManager.CreateCardRecord(card);
            hero.WeaponCardRecordID = weaponRecord.CardRecordID;

            CardManager.Instance.FindCard(1, out card);
            CardRecord servant1Record = game.GameCardManager.CreateCardRecord(card);
            game.Field2.AddCard(servant1Record.CardRecordID, 0);

            Assert.IsTrue(hero.AttackServant((servant1Record as ServantCardRecord), game.GamePlayer1));
            Assert.AreEqual(0, (servant1Record as ServantCardRecord).RemainedHealth);
            Assert.AreEqual(1, hero.AttackCountInThisTurn);
            Assert.AreEqual(7, (weaponRecord as WeaponCardRecord).Durability);

            game.Field2.RemoveCard(servant1Record.CardRecordID);

            CardManager.Instance.FindCard(8, out card);
            CardRecord servant2Record = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field2.AddCard(servant2Record.CardRecordID, 0));

            Assert.IsTrue(hero.AttackServant((servant2Record as ServantCardRecord), game.GamePlayer1));
            Assert.AreEqual(28, hero.RemainedHP);
            Assert.AreEqual(2, hero.AttackCountInThisTurn);
            Assert.AreEqual(6, (weaponRecord as WeaponCardRecord).Durability);
        }
    }
}

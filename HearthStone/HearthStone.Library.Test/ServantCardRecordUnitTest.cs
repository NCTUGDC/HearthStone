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

        [TestMethod]
        public void AttackHeroTestMethod1()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field1.AddCard(record.CardRecordID, 0));
            Assert.IsTrue((record as ServantCardRecord).AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
        }
        [TestMethod]
        public void AttackHeroTestMethod2()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field1.AddCard(record.CardRecordID, 0));
            Assert.IsTrue((record as ServantCardRecord).AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
            Assert.AreEqual(1, (record as ServantCardRecord).AttackCountInThisTurn);
            Assert.IsFalse((record as ServantCardRecord).AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
            Assert.AreEqual(1, (record as ServantCardRecord).AttackCountInThisTurn);
        }
        [TestMethod]
        public void AttackHeroTestMethod3()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord attackServantRecord = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field1.AddCard(attackServantRecord.CardRecordID, 0));
            CardManager.Instance.FindCard(8, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field2.AddCard(record.CardRecordID, 0));
            Assert.IsTrue(game.Field2.AnyTauntServant());
            Assert.IsFalse((attackServantRecord as ServantCardRecord).AttackHero(game.GamePlayer2.Hero, game.GamePlayer1));
            Assert.AreEqual(0, (attackServantRecord as ServantCardRecord).AttackCountInThisTurn);
            Assert.AreEqual(30, game.GamePlayer2.Hero.RemainedHP);
        }
        [TestMethod]
        public void AttackServantTestMethod1()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord attackServantRecord = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field1.AddCard(attackServantRecord.CardRecordID, 0));
            CardManager.Instance.FindCard(1, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            game.Field2.AddCard(record.CardRecordID, 0);
            Assert.IsTrue((attackServantRecord as ServantCardRecord).AttackServant(record as ServantCardRecord, game.GamePlayer1));
        }
        [TestMethod]
        public void AttackServantTestMethod2()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord attackServantRecord = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field1.AddCard(attackServantRecord.CardRecordID, 0));
            CardManager.Instance.FindCard(1, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            game.Field2.AddCard(record.CardRecordID, 0);
            Assert.IsTrue((attackServantRecord as ServantCardRecord).AttackServant(record as ServantCardRecord, game.GamePlayer1));
            Assert.IsFalse((attackServantRecord as ServantCardRecord).AttackServant(record as ServantCardRecord, game.GamePlayer1));
        }
        [TestMethod]
        public void AttackServantTestMethod3()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord attackServantRecord = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field1.AddCard(attackServantRecord.CardRecordID, 0));
            CardManager.Instance.FindCard(8, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            game.Field2.AddCard(record.CardRecordID, 0);
            Assert.IsTrue(game.Field2.AnyTauntServant());
            Assert.IsTrue((attackServantRecord as ServantCardRecord).AttackServant(record as ServantCardRecord, game.GamePlayer1));
        }
        [TestMethod]
        public void AttackServantTestMethod4()
        {
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord attackServantRecord = game.GameCardManager.CreateCardRecord(card);
            Assert.IsTrue(game.Field1.AddCard(attackServantRecord.CardRecordID, 0));
            CardManager.Instance.FindCard(8, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            CardManager.Instance.FindCard(7, out card);
            CardRecord record2 = game.GameCardManager.CreateCardRecord(card);
            game.Field2.AddCard(record.CardRecordID, 0);
            Assert.IsTrue(game.Field2.AnyTauntServant());
            Assert.IsFalse((attackServantRecord as ServantCardRecord).AttackServant(record2 as ServantCardRecord, game.GamePlayer1));
        }
    }
}

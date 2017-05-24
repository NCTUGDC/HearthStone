using HearthStone.Library.Effects;
using HearthStone.Library.Effectors;
using HearthStone.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    class TestEffect : Effect
    {
        public TestEffect(int effectID) : base(effectID)
        {

        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.Test;
            }
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "Test Effect";
        }
    }

    [TestClass]
    public class EffectUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Effect effect = new TestEffect(1);

            Assert.IsNotNull(effect);
            Assert.AreEqual(effect.EffectID, 1);
            Assert.AreEqual(effect.EffectType, EffectTypeCode.Test);
            Assert.AreEqual(effect.Description(null, 0), "Test Effect");
        }
        [TestMethod]
        public void DescriptionTestMethod1()
        {
            Assert.AreEqual("衝鋒", new ChargeEffect(1).Description(null, 0));
            Assert.AreEqual("造成1點傷害", new DealSpellDamageEffect(1, 1).Description(null, 0));
            Assert.AreEqual("摧毀一個敵方手下", new DestroyEnemyMinionEffect(1).Description(null, 0));
            Assert.AreEqual("使一個手下的攻擊力加倍", new DoubleMinionAttackEffect(1).Description(null, 0));
            Assert.AreEqual("使一個手下的生命值加倍", new DoubleMinionHealthEffect(1).Description(null, 0));
            Assert.AreEqual("抽1張牌", new DrawCardEffect(1, 1).Description(null, 0));
            Assert.AreEqual("賦予一個手下+1攻擊", new GiveMinionAttackBuffEffect(1, 1).Description(null, 0));
            Assert.AreEqual("賦予一個手下+2生命", new GiveMinionHealthBuffEffect(1, 2).Description(null, 0));
            Assert.AreEqual("恢復5點生命值", new RestoreHealthEffect(1, 5).Description(null, 0));
            Assert.AreEqual("沉默一個手下", new SilenceMinionEffect(1).Description(null, 0));
            Assert.AreEqual("法術傷害+2", new SpellDamageEffect(1, 2).Description(null, 0));
            Assert.AreEqual("嘲諷", new TauntEffect(1).Description(null, 0));
            Assert.AreEqual("風怒", new WindfuryEffect(1).Description(null, 0));
        }
        [TestMethod]
        public void DescriptionTestMethod2()
        {
            Assert.AreEqual("造成1點傷害", new DealSpellDamageEffect(1, 1).Description(GameUnitTest.InitialGameStatus(), 0));
            Assert.AreEqual("造成1點傷害", new DealSpellDamageEffect(1, 1).Description(GameUnitTest.InitialGameStatus(), 1));
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(11, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            game.Field1.AddCard(record.CardRecordID, 0);
            Assert.AreEqual("造成*2點傷害", new DealSpellDamageEffect(1, 1).Description(game, 1));
            CardRecord record2 = game.GameCardManager.CreateCardRecord(card);
            game.Field1.AddCard(record2.CardRecordID, 0);
            Assert.AreEqual("造成*3點傷害", new DealSpellDamageEffect(1, 1).Description(game, 1));
            Effect effect;
            CardManager.Instance.FindEffect(6, out effect);
            Effector effector = game.GameCardManager.CreareEffector(effect);
            record2.AddEffector(effector.EffectorID);
            Assert.AreEqual("造成*5點傷害", new DealSpellDamageEffect(1, 1).Description(game, 1));
            game.GameCardManager.LoadEffector(new SpellDamageEffector(0, 0));
            record2.AddEffector(0);
            Assert.AreEqual("造成*5點傷害", new DealSpellDamageEffect(1, 1).Description(game, 1));
            game.GameCardManager.LoadEffector(new TauntEffector(10, 2));
            record2.AddEffector(10);
            Assert.AreEqual("造成*5點傷害", new DealSpellDamageEffect(1, 1).Description(game, 1));
        }
        [TestMethod]
        public void DescriptionTestMethod3()
        {
            Assert.AreEqual("對全部敵方手下造成1點傷害", new DealSpellDamageToAllEnemyMinionsEffect(1, 1).Description(GameUnitTest.InitialGameStatus(), 0));
            Assert.AreEqual("對全部敵方手下造成1點傷害", new DealSpellDamageToAllEnemyMinionsEffect(1, 1).Description(GameUnitTest.InitialGameStatus(), 1));
            Game game = GameUnitTest.InitialGameStatus();
            Card card;
            CardManager.Instance.FindCard(11, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            game.Field1.AddCard(record.CardRecordID, 0);
            Assert.AreEqual("對全部敵方手下造成*2點傷害", new DealSpellDamageToAllEnemyMinionsEffect(1, 1).Description(game, 1));
            CardRecord record2 = game.GameCardManager.CreateCardRecord(card);
            game.Field1.AddCard(record2.CardRecordID, 0);
            Assert.AreEqual("對全部敵方手下造成*3點傷害", new DealSpellDamageToAllEnemyMinionsEffect(1, 1).Description(game, 1));
            Effect effect;
            CardManager.Instance.FindEffect(6, out effect);
            Effector effector = game.GameCardManager.CreareEffector(effect);
            record2.AddEffector(effector.EffectorID);
            Assert.AreEqual("對全部敵方手下造成*5點傷害", new DealSpellDamageToAllEnemyMinionsEffect(1, 1).Description(game, 1));
            game.GameCardManager.LoadEffector(new SpellDamageEffector(0, 0));
            record2.AddEffector(0);
            Assert.AreEqual("對全部敵方手下造成*5點傷害", new DealSpellDamageToAllEnemyMinionsEffect(1, 1).Description(game, 1));
            game.GameCardManager.LoadEffector(new TauntEffector(10, 2));
            record2.AddEffector(10);
            Assert.AreEqual("對全部敵方手下造成*5點傷害", new DealSpellDamageToAllEnemyMinionsEffect(1, 1).Description(game, 1));
        }
    }
}

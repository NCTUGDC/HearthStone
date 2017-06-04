using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace HearthStone.Library.Test.GameSystemTest
{
    [TestClass]
    public class Test4
    {
        [TestMethod]
        public void Episode_ServantAttack_衝鋒效果_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"衝鋒手下"*1 法力水晶10/10
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"衝鋒手下"
            //確認手牌與法力水晶變化
            //"衝鋒手下"攻擊敵方手下A
            //確認敵方場上手下的血量與生存狀況
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var servantCards = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 7, 1, 1 });
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, new List<int> { servantCards[0].CardRecordID });
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 10, 10);
            GameSystemTestEnvironment.GameWithFieldState(game, new List<int>(), new List<int> { servantCards[1].CardRecordID, servantCards[2].CardRecordID });
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(2, game.Field2.ServantCount);
            Assert.AreEqual(2, servantCards[1].RemainedHealth);
            Assert.AreEqual(2, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(10, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations 玩家1出"衝鋒手下"
            Assert.IsTrue(game.NonTargetDisplayServant(1, servantCards[0].CardRecordID, 0));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(1, game.Field1.ServantCount);
            Assert.AreEqual(2, game.Field2.ServantCount);
            Assert.IsTrue(servantCards[0].IsDisplayInThisTurn);
            Assert.AreEqual(0, servantCards[0].AttackCountInThisTurn);
            Assert.AreEqual(1, servantCards[0].RemainedHealth);
            Assert.AreEqual(2, servantCards[1].RemainedHealth);
            Assert.AreEqual(2, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(9, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations "衝鋒手下"攻擊敵方手下A
            Assert.IsTrue(game.ServantAttack(1, servantCards[0].CardRecordID, servantCards[1].CardRecordID, true));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(0, game.Field1.ServantCount);
            Assert.AreEqual(2, game.Field2.ServantCount);
            Assert.IsTrue(servantCards[0].IsDisplayInThisTurn);
            Assert.AreEqual(1, servantCards[0].AttackCountInThisTurn);
            Assert.AreEqual(0, servantCards[0].RemainedHealth);
            Assert.AreEqual(1, servantCards[1].RemainedHealth);
            Assert.AreEqual(2, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(9, game.GamePlayer1.RemainedManaCrystal);
            #endregion
        }
        [TestMethod]
        public void Episode_ServantAttack_嘲諷效果_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"嘲諷手下"*1 法力水晶10/10
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"嘲諷手下"
            //確認我方場地有嘲諷手下
            //"嘲諷手下"攻擊"手下A" -失敗
            //確認手牌與法力水晶變化
            //結束回合
            //確認回合數與當前玩家
            //確認手牌與法力水晶變化
            //"手下A"攻擊玩家1英雄 -失敗
            //確認手下A與玩家1英雄的血量與手下A本回合攻擊次數
            //"手下A"攻擊嘲諷手下
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_ServantAttack_風怒效果_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"風怒手下"*1 法力水晶10/10
            //輪到1號玩家 玩家1出"風怒手下"
            //"風怒手下"攻擊敵方英雄 -失敗
            //確認手牌與法力水晶變化
            //結束回合
            //確認回合數與當前玩家
            //確認手牌與法力水晶變化
            //"風怒手下"攻擊玩家2英雄 -失敗
            //結束回合
            //"風怒手下"攻擊玩家2英雄
            //確認"風怒手下"與玩家2英雄的血量與"風怒手下"本回合攻擊次數
            //"風怒手下"攻擊玩家2英雄
            //確認"風怒手下"與玩家2英雄的血量與"風怒手下"本回合攻擊次數
            //"風怒手下"攻擊玩家2英雄 -失敗
            //確認"風怒手下"與玩家2英雄的血量與"風怒手下"本回合攻擊次數
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_AOE1_手下法傷效果_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"法傷手下"*1 "傷害A"*2 法力水晶10/10
            //輪到1號玩家 玩家1出"法傷手下"
            //確認手牌與法力水晶變化
            //玩家1出"傷害A"對"法傷手下"造成效果
            //確認手牌與法力水晶變化
            //確認場上手下的血量與生存狀況
            //玩家1出"傷害A"對敵方英雄造成效果
            //確認手牌與法力水晶變化
            //確認敵方英雄的血量
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_AOE1_武器法傷效果_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"法傷武器"*1 "傷害A"*2 法力水晶10/10
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"法傷武器"
            //確認手牌與法力水晶變化
            //玩家1出"傷害A"對敵方英雄造成效果
            //確認手牌與法力水晶變化
            //確認敵方英雄的血量
            //玩家1出"傷害A"對"手下A"造成效果
            //確認手牌與法力水晶變化
            //確認場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_AOE1_武器與手下法傷效果_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"法傷手下"*1 "法傷武器"*1 "傷害A"*2 法力水晶10/10
            //輪到1號玩家 玩家1出"法傷手下"
            //確認手牌與法力水晶變化
            //玩家1出"傷害A"對敵方英雄造成效果
            //確認手牌與法力水晶變化
            //確認敵方英雄的血量
            //玩家1出"法傷武器"
            //確認手牌與法力水晶變化
            //玩家1出"傷害A"對敵方英雄造成效果
            //確認手牌與法力水晶變化
            //確認敵方英雄的血量
            Assert.Fail();
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace HearthStone.Library.Test.GameSystemTest
{
    [TestClass]
    public class Test2
    {
        [TestMethod]
        public void Episode_CastSpell_AOE1_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"AOE1"*1 法力水晶10/10
            //玩家2場上有 手下A*1 手下B*1 手下C*1
            //輪到1號玩家 玩家1出"AOE1"
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var spellCards = GameSystemTestEnvironment.GameWithSpellCardRecordState(game, new List<int>
            { 15 });
            var servantCards = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3 });
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, new List<int> { spellCards[0].CardRecordID });
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 10, 10);
            GameSystemTestEnvironment.GameWithFieldState(game, new List<int>(), new List<int> { servantCards[0].CardRecordID, servantCards[1].CardRecordID, servantCards[2].CardRecordID });
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(3, game.Field2.ServantCount);
            Assert.AreEqual(2, servantCards[0].RemainedHealth);
            Assert.AreEqual(3, servantCards[1].RemainedHealth);
            Assert.AreEqual(4, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(10, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations 玩家1出"AOE1"
            Assert.IsTrue(game.NonTargeCasttSpell(1, spellCards[0].CardRecordID));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(3, game.Field2.ServantCount);
            Assert.AreEqual(1, servantCards[0].RemainedHealth);
            Assert.AreEqual(2, servantCards[1].RemainedHealth);
            Assert.AreEqual(3, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(8, game.GamePlayer1.RemainedManaCrystal);
            #endregion
        }
        [TestMethod]
        public void Episode_CastSpell_AOE2_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"AOE2"*1 法力水晶10/10
            //玩家2場上有 手下A*1 手下B*1 手下C*1
            //輪到1號玩家 玩家1出"AOE2"
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var spellCards = GameSystemTestEnvironment.GameWithSpellCardRecordState(game, new List<int>
            { 16 });
            var servantCards = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3 });
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, new List<int> { spellCards[0].CardRecordID });
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 10, 10);
            GameSystemTestEnvironment.GameWithFieldState(game, new List<int>(), new List<int> { servantCards[0].CardRecordID, servantCards[1].CardRecordID, servantCards[2].CardRecordID });
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(3, game.Field2.ServantCount);
            Assert.AreEqual(2, servantCards[0].RemainedHealth);
            Assert.AreEqual(3, servantCards[1].RemainedHealth);
            Assert.AreEqual(4, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(10, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations 玩家1出"AOE2"
            Assert.IsTrue(game.NonTargeCasttSpell(1, spellCards[0].CardRecordID));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(2, game.Field2.ServantCount);
            Assert.AreEqual(0, servantCards[0].RemainedHealth);
            Assert.AreEqual(1, servantCards[1].RemainedHealth);
            Assert.AreEqual(2, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(6, game.GamePlayer1.RemainedManaCrystal);
            #endregion
        }
        [TestMethod]
        public void Episode_CastSpell_AOE3_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"AOE3"*1 法力水晶10/10
            //玩家2場上有 手下A*1 手下B*1 手下C*1
            //輪到1號玩家 玩家1出"AOE3"
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var spellCards = GameSystemTestEnvironment.GameWithSpellCardRecordState(game, new List<int>
            { 17 });
            var servantCards = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3 });
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, new List<int> { spellCards[0].CardRecordID });
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 10, 10);
            GameSystemTestEnvironment.GameWithFieldState(game, new List<int>(), new List<int> { servantCards[0].CardRecordID, servantCards[1].CardRecordID, servantCards[2].CardRecordID });
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(3, game.Field2.ServantCount);
            Assert.AreEqual(2, servantCards[0].RemainedHealth);
            Assert.AreEqual(3, servantCards[1].RemainedHealth);
            Assert.AreEqual(4, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(10, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations 玩家1出"AOE3"
            Assert.IsTrue(game.NonTargeCasttSpell(1, spellCards[0].CardRecordID));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(0, game.Field2.ServantCount);
            Assert.AreEqual(-2, servantCards[0].RemainedHealth);
            Assert.AreEqual(-1, servantCards[1].RemainedHealth);
            Assert.AreEqual(0, servantCards[2].RemainedHealth);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(3, game.GamePlayer1.RemainedManaCrystal);
            #endregion
        }
        [TestMethod]
        public void Episode_CastSpell_傷害A_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"傷害A"*1 法力水晶10/10
            //玩家2場上有 手下F*2
            //輪到1號玩家 玩家1出"傷害A"對位置0的手下釋放效果
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_傷害B_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"傷害B"*1 法力水晶10/10
            //玩家2場上有 手下F*2
            //輪到1號玩家 玩家1出"傷害B"對位置0的手下釋放效果
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_傷害C_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"傷害C"*1 法力水晶10/10
            //玩家2場上有 手下F*2
            //輪到1號玩家 玩家1出"傷害C"對位置1的手下釋放效果
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_傷害D_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"傷害D"*1 法力水晶10/10
            //玩家2場上有 手下F*2
            //輪到1號玩家 玩家1出"傷害D"對敵方英雄釋放效果
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            //確認敵方英雄血量狀況
            Assert.Fail();
        }
    }
}

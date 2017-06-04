using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace HearthStone.Library.Test.GameSystemTest
{
    [TestClass]
    public class Test3
    {
        [TestMethod]
        public void Episode_CastSpell_抽牌1_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"抽牌1"*1 牌組有CardID1~6的卡各1張 法力水晶10/10
            //輪到1號玩家 玩家1出"抽牌1"
            //確認手牌與法力水晶變化
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var spellCards = GameSystemTestEnvironment.GameWithSpellCardRecordState(game, new List<int>
            { 18 });
            var servantCards = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3, 4, 5, 6 });
            GameSystemTestEnvironment.GameWithGamePlayerDeckState(game, 1, servantCards.Select(x => x.CardRecordID).ToList());
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, new List<int> { spellCards[0].CardRecordID });
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 10, 10);
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(6, game.GamePlayer1.Deck.CardRecordIDs.Count());
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(10, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations 玩家1出"抽牌1"
            Assert.IsTrue(game.NonTargeCasttSpell(1, spellCards[0].CardRecordID));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(4, game.GamePlayer1.Deck.CardRecordIDs.Count());
            Assert.AreEqual(2, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(7, game.GamePlayer1.RemainedManaCrystal);
            int[] handCardIDs = game.GamePlayer1.HandCardIDs.ToArray();
            Assert.AreEqual(handCardIDs[0], servantCards[0].CardRecordID);
            Assert.AreEqual(handCardIDs[1], servantCards[1].CardRecordID);
            #endregion
        }
        [TestMethod]
        public void Episode_CastSpell_抽牌2_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"抽牌2"*1 牌組有CardID1~6的卡各1張 法力水晶10/10
            //輪到1號玩家 玩家1出"抽牌2"
            //確認手牌與法力水晶變化
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var spellCards = GameSystemTestEnvironment.GameWithSpellCardRecordState(game, new List<int>
            { 19 });
            var servantCards = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3, 4, 5, 6 });
            GameSystemTestEnvironment.GameWithGamePlayerDeckState(game, 1, servantCards.Select(x => x.CardRecordID).ToList());
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, new List<int> { spellCards[0].CardRecordID });
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 10, 10);
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(6, game.GamePlayer1.Deck.CardRecordIDs.Count());
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(10, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations 玩家1出"抽牌1"
            Assert.IsTrue(game.NonTargeCasttSpell(1, spellCards[0].CardRecordID));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(3, game.GamePlayer1.Deck.CardRecordIDs.Count());
            Assert.AreEqual(3, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(5, game.GamePlayer1.RemainedManaCrystal);
            int[] handCardIDs = game.GamePlayer1.HandCardIDs.ToArray();
            Assert.AreEqual(handCardIDs[0], servantCards[0].CardRecordID);
            Assert.AreEqual(handCardIDs[1], servantCards[1].CardRecordID);
            Assert.AreEqual(handCardIDs[2], servantCards[2].CardRecordID);
            #endregion
        }
        [TestMethod]
        public void Episode_CastSpell_摧毀_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"摧毀"*1 法力水晶10/10
            //玩家2場上有 手下A*1 手下B*1 手下C*1
            //輪到1號玩家 玩家1出"摧毀" 對象為敵方位置1的手下
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var spellCards = GameSystemTestEnvironment.GameWithSpellCardRecordState(game, new List<int>
            { 24 });
            var servantCards = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3 });
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, new List<int> { spellCards[0].CardRecordID });
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 10, 10);
            GameSystemTestEnvironment.GameWithFieldState(game, new List<int>(), servantCards.Select(x => x.CardRecordID).ToList());
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

            #region operations 玩家1出"摧毀" 對象為敵方位置1的手下
            Assert.IsTrue(game.TargetCastSpell(1, spellCards[0].CardRecordID, servantCards[0].CardRecordID, true));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(2, game.Field2.ServantCount);
            Assert.AreEqual(2, servantCards[0].RemainedHealth);
            Assert.AreEqual(3, servantCards[1].RemainedHealth);
            Assert.AreEqual(4, servantCards[2].RemainedHealth);
            int positionIndex1ServantID;
            Assert.IsTrue(game.Field2.FindCardWithPositionIndex(1, out positionIndex1ServantID));
            Assert.AreEqual(servantCards[2].CardRecordID, positionIndex1ServantID);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(5, game.GamePlayer1.RemainedManaCrystal);
            #endregion
        }
        [TestMethod]
        public void Episode_CastSpell_生命加倍_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"生命加倍"*2 法力水晶10/10
            //玩家1場上有 手下A*1
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"生命加倍" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //玩家1出"生命加倍" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var spellCards = GameSystemTestEnvironment.GameWithSpellCardRecordState(game, new List<int>
            { 26, 26 });
            var servantCards = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 1 });
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, spellCards.Select(x => x.CardRecordID).ToList());
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 10, 10);
            GameSystemTestEnvironment.GameWithFieldState(game, new List<int> { servantCards[0].CardRecordID }, new List<int> { servantCards[1].CardRecordID });
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(2, servantCards[0].RemainedHealth);
            Assert.AreEqual(2, servantCards[0].Health);
            Assert.AreEqual(2, servantCards[1].RemainedHealth);
            Assert.AreEqual(2, servantCards[1].Health);
            #endregion

            #region player1
            Assert.AreEqual(2, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(10, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations 玩家1出"生命加倍" 對象為我方的手下
            Assert.IsTrue(game.TargetCastSpell(1, spellCards[0].CardRecordID, servantCards[0].CardRecordID, true));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(4, servantCards[0].RemainedHealth);
            Assert.AreEqual(4, servantCards[0].Health);
            Assert.AreEqual(2, servantCards[1].RemainedHealth);
            Assert.AreEqual(2, servantCards[1].Health);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(8, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations 玩家1出"生命加倍" 對象為敵方的手下
            Assert.IsTrue(game.TargetCastSpell(1, spellCards[1].CardRecordID, servantCards[1].CardRecordID, true));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(4, servantCards[0].RemainedHealth);
            Assert.AreEqual(4, servantCards[0].Health);
            Assert.AreEqual(4, servantCards[1].RemainedHealth);
            Assert.AreEqual(4, servantCards[1].Health);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(10, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(6, game.GamePlayer1.RemainedManaCrystal);
            #endregion
        }
        [TestMethod]
        public void Episode_CastSpell_攻擊加倍_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"攻擊加倍"*2 法力水晶10/10
            //玩家1場上有 手下A*1
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"攻擊加倍" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //玩家1出"攻擊加倍" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_手下加強A_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"手下加強A"*2 法力水晶10/10
            //玩家1場上有 手下A*1
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"手下加強A" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //玩家1出"手下加強A" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_手下加強B_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"手下加強B"*2 法力水晶10/10
            //玩家1場上有 手下A*1
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"手下加強B" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //玩家1出"手下加強B" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_回血A_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"回血A"*4 法力水晶10/10
            //玩家1場上有 手下F*1(剩3血) 英雄血量為10
            //玩家2場上有 手下F*1(剩1血) 英雄血量為28
            //輪到1號玩家 玩家1出"回血A" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //確認雙方英雄的血量
            //玩家1出"回血A" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //確認雙方英雄的血量
            //玩家1出"回血A" 對象為敵方的英雄
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //確認雙方英雄的血量
            //玩家1出"回血A" 對象為我方的英雄
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //確認雙方英雄的血量
            Assert.Fail();
        }
    }
}

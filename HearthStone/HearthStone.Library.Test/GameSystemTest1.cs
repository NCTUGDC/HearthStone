using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class GameSystemTest1
    {
        [TestMethod]
        public void Episode_EmptyGameTestMethod1()
        {//開始一場空的遊戲 輪到1號玩家
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 0);
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(0, game.Field1.ServantCount);
            Assert.AreEqual(0, game.Field2.ServantCount);
            Assert.AreEqual(0, game.RoundCount);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.Deck.CardRecordIDs.Count());
            Assert.AreEqual(1, game.GamePlayer1.GamePlayerID);
            Assert.AreEqual(false, game.GamePlayer1.HasChangedHand);
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(0, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.Hero.Attack);
            Assert.AreEqual(0, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(1, game.GamePlayer1.Hero.HeroID);
            Assert.AreEqual(30, game.GamePlayer1.Hero.HP);
            Assert.AreEqual(30, game.GamePlayer1.Hero.RemainedHP);
            Assert.AreEqual(0, game.GamePlayer1.Hero.WeaponCardRecordID);
            #endregion

            #region player2
            Assert.AreEqual(0, game.GamePlayer2.Deck.CardRecordIDs.Count());
            Assert.AreEqual(2, game.GamePlayer2.GamePlayerID);
            Assert.AreEqual(false, game.GamePlayer2.HasChangedHand);
            Assert.AreEqual(0, game.GamePlayer2.HandCardIDs.Count());
            Assert.AreEqual(0, game.GamePlayer2.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer2.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer2.Hero.Attack);
            Assert.AreEqual(0, game.GamePlayer2.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer2.Hero.HeroID);
            Assert.AreEqual(30, game.GamePlayer2.Hero.HP);
            Assert.AreEqual(30, game.GamePlayer2.Hero.RemainedHP);
            Assert.AreEqual(0, game.GamePlayer2.Hero.WeaponCardRecordID);
            #endregion
        }
        [TestMethod]
        public void Episode_SwapHandThenStartTestMethod1()
        {   //開始一場空的遊戲 輪到1號玩家 玩家1換手牌 玩家2換手牌
            //雙方換完手牌 遊戲開始 1號玩家抽一張牌 法力水晶上限+1 並補滿法力水晶
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 0);
            #endregion

            #region operations
            game.GamePlayer1.ChangeHand(new int[0]);
            game.GamePlayer2.ChangeHand(new int[0]);
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(0, game.Field1.ServantCount);
            Assert.AreEqual(0, game.Field2.ServantCount);
            Assert.AreEqual(1, game.RoundCount);
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.Deck.CardRecordIDs.Count());
            Assert.AreEqual(1, game.GamePlayer1.GamePlayerID);
            Assert.AreEqual(true, game.GamePlayer1.HasChangedHand);
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(1, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(1, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.Hero.Attack);
            Assert.AreEqual(0, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(1, game.GamePlayer1.Hero.HeroID);
            Assert.AreEqual(30, game.GamePlayer1.Hero.HP);
            Assert.AreEqual(30, game.GamePlayer1.Hero.RemainedHP);
            Assert.AreEqual(0, game.GamePlayer1.Hero.WeaponCardRecordID);
            #endregion

            #region player2
            Assert.AreEqual(0, game.GamePlayer2.Deck.CardRecordIDs.Count());
            Assert.AreEqual(2, game.GamePlayer2.GamePlayerID);
            Assert.AreEqual(true, game.GamePlayer2.HasChangedHand);
            Assert.AreEqual(0, game.GamePlayer2.HandCardIDs.Count());
            Assert.AreEqual(0, game.GamePlayer2.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer2.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer2.Hero.Attack);
            Assert.AreEqual(0, game.GamePlayer2.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer2.Hero.HeroID);
            Assert.AreEqual(30, game.GamePlayer2.Hero.HP);
            Assert.AreEqual(30, game.GamePlayer2.Hero.RemainedHP);
            Assert.AreEqual(0, game.GamePlayer2.Hero.WeaponCardRecordID);
            #endregion
        }
        [TestMethod]
        public void Episode_StartGameWithDeckTestMethod1()
        {
            //開始一場空的遊戲 雙方牌組都依序放置1~6號卡1張 輪到1號玩家 玩家1換手牌 玩家2換手牌
            //雙方換完手牌 遊戲開始 1號玩家抽一張牌 法力水晶上限+1 並補滿法力水晶
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 0);
            var servantCards1 = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3, 4, 5, 6 });
            GameSystemTestEnvironment.GameWithGamePlayerDeckState(game, 1, servantCards1.Select(x => x.CardRecordID).ToList());
            var servantCards2 = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3, 4, 5, 6 });
            GameSystemTestEnvironment.GameWithGamePlayerDeckState(game, 2, servantCards2.Select(x => x.CardRecordID).ToList());
            #endregion

            #region operations
            game.GamePlayer1.ChangeHand(new int[0]);
            game.GamePlayer2.ChangeHand(new int[0]);
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(0, game.Field1.ServantCount);
            Assert.AreEqual(0, game.Field2.ServantCount);
            Assert.AreEqual(1, game.RoundCount);
            #endregion

            #region player1
            Assert.AreEqual(5, game.GamePlayer1.Deck.CardRecordIDs.Count());
            Assert.AreEqual(1, game.GamePlayer1.GamePlayerID);
            Assert.AreEqual(true, game.GamePlayer1.HasChangedHand);
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(1, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(1, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.Hero.Attack);
            Assert.AreEqual(0, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(1, game.GamePlayer1.Hero.HeroID);
            Assert.AreEqual(30, game.GamePlayer1.Hero.HP);
            Assert.AreEqual(30, game.GamePlayer1.Hero.RemainedHP);
            Assert.AreEqual(0, game.GamePlayer1.Hero.WeaponCardRecordID);
            #endregion

            #region player2
            Assert.AreEqual(6, game.GamePlayer2.Deck.CardRecordIDs.Count());
            Assert.AreEqual(2, game.GamePlayer2.GamePlayerID);
            Assert.AreEqual(true, game.GamePlayer2.HasChangedHand);
            Assert.AreEqual(0, game.GamePlayer2.HandCardIDs.Count());
            Assert.AreEqual(0, game.GamePlayer2.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer2.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer2.Hero.Attack);
            Assert.AreEqual(0, game.GamePlayer2.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer2.Hero.HeroID);
            Assert.AreEqual(30, game.GamePlayer2.Hero.HP);
            Assert.AreEqual(30, game.GamePlayer2.Hero.RemainedHP);
            Assert.AreEqual(0, game.GamePlayer2.Hero.WeaponCardRecordID);
            #endregion
        }
        [TestMethod]
        public void Episode_Display2ServantTestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"嘲諷手下"*1 "沉默手下"*1 法力水晶6/6
            //輪到1號玩家 玩家1出"嘲諷手下" 再出 "沉默手下"(指定"嘲諷手下")
            //"嘲諷手下"失去嘲諷效果
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 0);
            var servantCards1 = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 8, 10 });
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, servantCards1.Select(x => x.CardRecordID).ToList());
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 6, 6);
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(0, game.Field1.ServantCount);
            Assert.IsFalse(game.Field1.AnyTauntServant());
            #endregion

            #region player1
            Assert.AreEqual(2, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(6, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(6, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations
            Assert.IsTrue(game.NonTargetDisplayServant(1, 1, 0));
            #endregion

            #region game
            Assert.AreEqual(1, game.Field1.ServantCount);
            int displayedServantID;
            Assert.IsTrue(game.Field1.FindCardWithPositionIndex(0, out displayedServantID));
            Assert.AreEqual(1, displayedServantID);
            Assert.IsTrue(game.Field1.AnyTauntServant());
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(6, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(4, game.GamePlayer1.RemainedManaCrystal);
            #endregion

            #region operations
            Assert.IsTrue(game.TargetDisplayServant(1, 2, 1, 1, true));
            #endregion

            #region game
            Assert.AreEqual(2, game.Field1.ServantCount);
            Assert.IsTrue(game.Field1.FindCardWithPositionIndex(1, out displayedServantID));
            Assert.AreEqual(2, displayedServantID);
            Assert.IsFalse(game.Field1.AnyTauntServant());
            Assert.AreEqual(0, servantCards1[0].EffectorIDs.Count());
            #endregion

            #region player1
            Assert.AreEqual(0, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(6, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.RemainedManaCrystal);
            #endregion
        }
        [TestMethod]
        public void Episode_EquipWeaponAndAttackTestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"精良武器"*1 "風怒武器"*1 "法傷武器"*1 法力水晶8/8
            //輪到1號玩家 玩家1出"精良武器" 並攻擊敵方英雄
            //玩家1攻擊敵方英雄 -失敗
            //玩家1出"風怒武器" "精良武器"被破壞 並攻擊敵方英雄
            //玩家1攻擊敵方英雄 -失敗
            //玩家1出"法傷武器" -失敗
            //玩家1回合結束
            //玩家2回合結束
            #region initial
            Game game = GameSystemTestEnvironment.EmptyGame(1, 1);
            var weaponCards1 = GameSystemTestEnvironment.GameWithWeaponCardRecordState(game, new List<int>
            { 12, 13, 14 });
            GameSystemTestEnvironment.GameWithGamePlayerHandState(game, 1, weaponCards1.Select(x => x.CardRecordID).ToList());
            GameSystemTestEnvironment.GameWithGamePlayerManaCrystalState(game, 1, 8, 8);
            bool weapon1DestroyedFlag = false, weapon2DestroyedFlag = false;
            weaponCards1[0].OnDestroyed += (weapon) => { weapon1DestroyedFlag = true; };
            weaponCards1[1].OnDestroyed += (weapon) => { weapon2DestroyedFlag = true; };
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(3, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(8, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(0, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(0, game.GamePlayer1.Hero.AttackWithWeapon(game));
            #endregion

            #region player2
            Assert.AreEqual(30, game.GamePlayer2.Hero.RemainedHP);
            #endregion

            #region operations 玩家1出"精良武器"
            Assert.IsTrue(game.NonTargetEquipWeapon(1, 1));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(2, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(6, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(1, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(3, weaponCards1[0].Durability, 3);
            Assert.IsFalse(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(30, game.GamePlayer2.Hero.RemainedHP);
            #endregion

            #region operations 並攻擊敵方英雄
            Assert.IsTrue(game.HeroAttack(1, 2, false));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(2, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(6, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(1, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(1, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(2, weaponCards1[0].Durability, 3);
            Assert.IsFalse(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(28, game.GamePlayer2.Hero.RemainedHP);
            #endregion

            #region operations 玩家1攻擊敵方英雄 -失敗
            Assert.IsFalse(game.HeroAttack(1, 2, false));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(2, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(6, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(1, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(1, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(2, weaponCards1[0].Durability);
            Assert.IsFalse(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(28, game.GamePlayer2.Hero.RemainedHP);
            #endregion

            #region operations 玩家1出"風怒武器" "精良武器"被破壞
            Assert.IsTrue(game.NonTargetEquipWeapon(1, 2));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(1, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(8, weaponCards1[1].Durability);
            Assert.IsTrue(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(28, game.GamePlayer2.Hero.RemainedHP);
            #endregion

            #region operations 並攻擊敵方英雄
            Assert.IsTrue(game.HeroAttack(1, 2, false));
            #endregion
            
            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(7, weaponCards1[1].Durability);
            Assert.IsTrue(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(26, game.GamePlayer2.Hero.RemainedHP);
            #endregion
            
            #region operations 玩家1攻擊敵方英雄 -失敗
            Assert.IsFalse(game.HeroAttack(1, 2, false));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(7, weaponCards1[1].Durability);
            Assert.IsTrue(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(26, game.GamePlayer2.Hero.RemainedHP);
            #endregion
            
            #region operations 玩家1出"法傷武器" -失敗
            Assert.IsFalse(game.NonTargetEquipWeapon(1, 3));
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(7, weaponCards1[1].Durability);
            Assert.IsTrue(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(26, game.GamePlayer2.Hero.RemainedHP);
            #endregion
            
            #region operations 玩家1回合結束
            game.EndRound();
            #endregion

            #region game
            Assert.AreEqual(2, game.CurrentGamePlayerID);
            Assert.AreEqual(2, game.RoundCount);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(8, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(0, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(7, weaponCards1[1].Durability);
            Assert.IsTrue(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(26, game.GamePlayer2.Hero.RemainedHP);
            #endregion

            #region operations 玩家2回合結束
            game.EndRound();
            #endregion

            #region game
            Assert.AreEqual(1, game.CurrentGamePlayerID);
            Assert.AreEqual(3, game.RoundCount);
            #endregion

            #region player1
            Assert.AreEqual(1, game.GamePlayer1.HandCardIDs.Count());
            Assert.AreEqual(9, game.GamePlayer1.ManaCrystal);
            Assert.AreEqual(9, game.GamePlayer1.RemainedManaCrystal);
            Assert.AreEqual(0, game.GamePlayer1.Hero.AttackCountInThisTurn);
            Assert.AreEqual(2, game.GamePlayer1.Hero.WeaponCardRecordID);
            Assert.AreEqual(2, game.GamePlayer1.Hero.AttackWithWeapon(game));
            Assert.AreEqual(7, weaponCards1[1].Durability);
            Assert.IsTrue(weapon1DestroyedFlag);
            Assert.IsFalse(weapon2DestroyedFlag);
            #endregion

            #region player2
            Assert.AreEqual(26, game.GamePlayer2.Hero.RemainedHP);
            #endregion
        }
    }
}

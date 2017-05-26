using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class GameSystemTest1
    {
        [TestMethod]
        public void EmptyGameTestMethod1()
        {
            #region game
            Game game = GameSystemTestEnvironment.EmptyGame(1, 0);
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
        public void SwapHandThenStartTestMethod1()
        {
            #region game
            Game game = GameSystemTestEnvironment.EmptyGame(1, 0);
            game.GamePlayer1.ChangeHand(new int[0]);
            game.GamePlayer2.ChangeHand(new int[0]);
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
        {
            Game game = GameSystemTestEnvironment.EmptyGame(1, 0);
            var servantCards1 = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3, 4, 5, 6 });
            GameSystemTestEnvironment.GameWithGamePlayerDeckState(game, 1, servantCards1.Select(x => x.CardRecordID).ToList());
            var servantCards2 = GameSystemTestEnvironment.GameWithServantCardRecordState(game, new List<int>
            { 1, 2, 3, 4, 5, 6 });
            GameSystemTestEnvironment.GameWithGamePlayerDeckState(game, 2, servantCards2.Select(x => x.CardRecordID).ToList());

            game.GamePlayer1.ChangeHand(new int[0]);
            game.GamePlayer2.ChangeHand(new int[0]);

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
    }
}

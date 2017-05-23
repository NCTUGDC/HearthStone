using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class GameTest
    {
        public static Game InitialGameStatus()
        {
            Deck deck1 = new Deck(1, "deck1", 20), deck2 = new Deck(2, "deck2", 20);           
            Game game = new Game(1, new Player(1, "player1"), new Player(2, "player2"), deck1, deck2);
            return game;
        }

        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Deck deck1 = new Deck(1, "test1", 10), deck2 = new Deck(2, "test2", 10);
            foreach(var card in CardManager.Instance.Cards)
            {
                deck1.AddCard(card);
                deck2.AddCard(card);
            }
            Game game = new Game(1, new Player(1, "test1"), new Player(2, "test2"), deck1, deck2);
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void NonTargetDisplayServantTestMethod1()
        {
            Game game = InitialGameStatus();
            GamePlayer currentGamePlayer = game.GamePlayer1;
            game.CurrentGamePlayerID = currentGamePlayer.GamePlayerID;
            currentGamePlayer.ManaCrystal = 10;
            currentGamePlayer.RemainedManaCrystal = 10;
            Card card;
            CardManager.Instance.FindCard(1, out card);
            CardRecord record = game.GameCardManager.CreateCardRecord(card);
            currentGamePlayer.AddHandCard(record.CardID);
            Assert.IsTrue(game.NonTargetDisplayServant(currentGamePlayer.GamePlayerID, record.CardRecordID, 0));
            Assert.AreEqual(9, currentGamePlayer.RemainedManaCrystal);
            int displayedCardRecordID;
            Assert.IsTrue(game.SelfField(currentGamePlayer.GamePlayerID).FindCardWithPositionIndex(0, out displayedCardRecordID));
            Assert.AreEqual(record.CardRecordID, displayedCardRecordID);
        }
    }
}

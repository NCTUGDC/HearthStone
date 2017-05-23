using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using HearthStone.Library.CardRecords;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class GamePlayerTest
    {
        [TestMethod]
        public void SerializationTestMethod1()
        {
            GameCardManager gameCardManager = new GameCardManager();
            GamePlayer gamePlayer = new GamePlayer(null, new Hero(1, 25, 30), new GameDeck(1, CardManager.Instance.Cards.Select(x => gameCardManager.CreateCardRecord(x).CardRecordID).ToList()));
            byte[] serializedData = SerializationHelper.Serialize(gamePlayer);
            GamePlayer deserializedGamePlayer = SerializationHelper.Deserialize<GamePlayer>(serializedData);
            Assert.IsNotNull(deserializedGamePlayer);

            Hero hero = deserializedGamePlayer.Hero;
            Assert.IsNotNull(hero);
            Assert.AreEqual(1, hero.HeroID);
            Assert.AreEqual(25, hero.RemainedHP);
            Assert.AreEqual(30, hero.HP);

            deserializedGamePlayer.ChangeHand(new int[0]);
            Assert.AreEqual(true, deserializedGamePlayer.HasChangedHand);

            GameDeck deck = deserializedGamePlayer.Deck;
            Assert.IsNotNull(deck);
            Assert.AreEqual(29, deck.CardRecordIDs.Count());
            foreach(var cardRecordID in deck.CardRecordIDs)
            {
                CardRecord record;
                Assert.IsTrue(gameCardManager.FindCard(cardRecordID, out record));
                Card settingCard;
                Assert.IsTrue(CardManager.Instance.FindCard(record.Card.CardID, out settingCard));
                Assert.AreEqual(settingCard.CardID, record.Card.CardID);
                Assert.AreEqual(settingCard.CardName, record.Card.CardName);
            }
        }
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            GamePlayer gamePlayer = new GamePlayer(null, null, null);
            Assert.IsNotNull(gamePlayer);
            Assert.IsNull(gamePlayer.Player);
            Assert.AreEqual(-1, gamePlayer.GamePlayerID);
            Assert.IsNull(gamePlayer.Hero);
            Assert.IsFalse(gamePlayer.HasChangedHand);
            Assert.AreEqual(0, gamePlayer.RemainedManaCrystal);
            Assert.AreEqual(0, gamePlayer.ManaCrystal);
            Assert.AreEqual(0, gamePlayer.HandCardIDs.Count());
            Assert.IsNull(gamePlayer.Deck);
            Assert.IsNotNull(gamePlayer.EventManager);
            Assert.IsNull(gamePlayer.Game);
        }
        [TestMethod]
        public void EventTestMethod1()
        {
            GamePlayer gamePlayer = new GamePlayer(null, null, null);
            int eventCounter = 0;
            gamePlayer.OnHandCardsChanged += (eventGamePlayer, cardRecordID, changeCode) => 
            {
                eventCounter++;
            };
            gamePlayer.AddHandCard(1);
            Assert.AreEqual(1, eventCounter);

            gamePlayer.OnHasChangedHandChanged += (eventGamePlayer) => 
            {
                eventCounter++;
            };
            gamePlayer.HasChangedHand = true;
            Assert.AreEqual(2, eventCounter);

            gamePlayer.OnManaCrystalChanged += (eventGamePlayer) =>
            {
                eventCounter++;
            };
            gamePlayer.ManaCrystal = 5;
            Assert.AreEqual(3, eventCounter);

            gamePlayer.OnRemainedManaCrystalChanged += (eventGamePlayer) =>
            {
                eventCounter++;
            };
            gamePlayer.RemainedManaCrystal = 2;
            Assert.AreEqual(4, eventCounter);
        }
        [TestMethod]
        public void AddHandCardTestMethod1()
        {
            GamePlayer gamePlayer = new GamePlayer(null, null, null);
            Assert.IsTrue(gamePlayer.AddHandCard(1));
            Assert.IsFalse(gamePlayer.AddHandCard(1));
            for(int i = 2; i <= 10; i++)
            {
                Assert.IsTrue(gamePlayer.AddHandCard(i));
            }
            Assert.IsFalse(gamePlayer.AddHandCard(11));
            gamePlayer.BindGame(GameTest.InitialGameStatus());
            gamePlayer.Game.GameCardManager.LoadCard(new SpellCardRecord(12, 1));
            Assert.IsFalse(gamePlayer.AddHandCard(12));
        }
        [TestMethod]
        public void RemoveHandCardTestMethod1()
        {
            GamePlayer gamePlayer = new GamePlayer(null, null, null);
            int eventCounter = 0;
            gamePlayer.OnHandCardsChanged += (eventGamePlayer, cardRecordID, changeCode) =>
            {
                if(changeCode == Protocol.DataChangeCode.Remove)
                    eventCounter++;
            };
            Assert.IsFalse(gamePlayer.RemoveHandCard(1));
            Assert.AreEqual(0, eventCounter);
            Assert.IsTrue(gamePlayer.AddHandCard(1));
            Assert.IsTrue(gamePlayer.RemoveHandCard(1));
            Assert.AreEqual(1, eventCounter);
        }
        [TestMethod]
        public void ChangeHandTestMethod1()
        {
            GamePlayer gamePlayer = new GamePlayer(null, null, new GameDeck(1, new System.Collections.Generic.List<int>
            {
                1, 2, 3, 4, 5, 6
            }));
            gamePlayer.AddHandCard(7);
            gamePlayer.AddHandCard(8);
            gamePlayer.AddHandCard(9);
            gamePlayer.ChangeHand(new int[] { 7, 8 });
            Assert.AreEqual(3, gamePlayer.HandCardIDs.Count());
            Assert.AreEqual(1, gamePlayer.HandCardIDs.Count(x => x == 9));
            Assert.AreEqual(0, gamePlayer.HandCardIDs.Count(x => x == 7));
            Assert.AreEqual(0, gamePlayer.HandCardIDs.Count(x => x == 8));
            Assert.AreEqual(1, gamePlayer.Deck.CardRecordIDs.Count(x => x == 7));
            Assert.AreEqual(1, gamePlayer.Deck.CardRecordIDs.Count(x => x == 8));
        }
    }
}

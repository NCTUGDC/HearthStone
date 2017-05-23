using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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
            Assert.AreEqual(28, deck.CardRecordIDs.Count());
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
    }
}

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
            int recordID = 0;
            GamePlayer gamePlayer = new GamePlayer(null, new Hero(1, 25, 30, true), new GameDeck(1, CardManager.Instance.Cards.Select(x => x.CreateRecord(recordID++)).ToList()));
            byte[] serializedData = SerializationHelper.Serialize(gamePlayer);
            GamePlayer deserializedGamePlayer = SerializationHelper.Deserialize<GamePlayer>(serializedData);
            Assert.IsNotNull(deserializedGamePlayer);

            Hero hero = deserializedGamePlayer.Hero;
            Assert.IsNotNull(hero);
            Assert.AreEqual(1, hero.HeroID);
            Assert.AreEqual(25, hero.HP);
            Assert.AreEqual(30, hero.MaxHP);
            Assert.AreEqual(true, hero.IsFrozen);

            deserializedGamePlayer.ChangeHand(new List<int>());
            Assert.AreEqual(true, deserializedGamePlayer.HasChangedHand);

            GameDeck deck = deserializedGamePlayer.Deck;
            Assert.IsNotNull(deck);
            Assert.AreEqual(28, deck.CardRecords.Count());
            foreach(var card in deck.CardRecords)
            {
                Card settingCard;
                Assert.IsTrue(CardManager.Instance.FindCard(card.Card.CardID, out settingCard));
                Assert.AreEqual(settingCard.CardID, card.Card.CardID);
                Assert.AreEqual(settingCard.CardName, card.Card.CardName);
            }
        }
    }
}

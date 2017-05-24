using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class CardManagerUnitTest
    {

        [TestMethod]
        public void CardManagerInstanceTestMethod1()
        {
            CardManager instance = CardManager.Instance;
            Assert.IsNotNull(instance, "CardManager.Instance should not be null");
        }

        [TestMethod]
        public void CardsEnumerableTestMethod1()
        {
            IEnumerable<Card> cards = CardManager.Instance.Cards;
            Assert.IsNotNull(cards, "CardManager.Instance.Cards should not be null");
        }


        [TestMethod]
        public void CardsEnumerableTestMethod2()
        {
            HashSet<int> IDs = new HashSet<int>();
            foreach (Card card in CardManager.Instance.Cards)
            {
                Assert.IsNotNull(card, "Card object in CardManager.Instance.Cards should not be null");
                Assert.IsFalse(IDs.Contains(card.CardID), "ID of Card objects in CardManager.Instance.Cards should not be the same");
                IDs.Add(card.CardID);
            }
        }


        [TestMethod]
        public void EffectsEnumerableTestMethod1()
        {
            IEnumerable<Effect> effects = CardManager.Instance.Effects;
            Assert.IsNotNull(effects, "CardManager.Instance.Effects should not be null");
        }

        [TestMethod]
        public void EffectsEnumerableTestMethod2()
        {
            HashSet<int> IDs = new HashSet<int>();
            foreach (Effect effect in CardManager.Instance.Effects)
            {
                Assert.IsNotNull(effect, "Effect object in CardManager.Instance.Effects should not be null");
                Assert.IsFalse(IDs.Contains(effect.EffectID), "ID of Effect objects in CardManager.Instance.Effects should not be the same");
                IDs.Add(effect.EffectID);
            }
        }

        [TestMethod]
        public void FindCardTestMethod1()
        {
            CardManager manager = CardManager.Instance;
            int minID = int.MaxValue;
            int maxID = int.MinValue;
            foreach (Card card in manager.Cards)
            {
                minID = Math.Min(minID, card.CardID);
                maxID = Math.Max(maxID, card.CardID);
                Card tempCard;
                bool isCardFound = manager.FindCard(card.CardID, out tempCard);
                Assert.IsTrue(isCardFound, "A valid card (retrived from CardManager.Instance.Cards) not found by FindCard method: " + card.CardID);
                Assert.IsNotNull(tempCard, "A valid card (retrived from CardManager.Instance.Cards) is null by FindCard method: " + card.CardID);
            }
        }
    }
}

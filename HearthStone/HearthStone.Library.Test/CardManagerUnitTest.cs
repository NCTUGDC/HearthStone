﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            foreach (Card card in CardManager.Instance.Cards)
            {
                Card tempCard;
                bool isCardFound = CardManager.Instance.FindCard(card.CardID, out tempCard);
                Assert.IsTrue(isCardFound, "A valid card (retrived from CardManager.Instance.Cards) not found by FindCard method: " + card.CardID);
                Assert.IsNotNull(tempCard, "A valid card (retrived from CardManager.Instance.Cards) is null by FindCard method: " + card.CardID);
            }
        }

        [TestMethod]
        public void FindCardTestMethod2()
        {
            int minID = int.MaxValue;
            int maxID = int.MinValue;
            foreach (Card card in CardManager.Instance.Cards)
            {
                minID = Math.Min(minID, card.CardID);
                maxID = Math.Max(maxID, card.CardID);
            }

            foreach (int id in new int[] { minID - 1, maxID + 1 })
            {
                Card tempCard;
                bool isCardFound = CardManager.Instance.FindCard(id, out tempCard);
                Assert.IsFalse(isCardFound, "An invalid card found by FindCard method: " + id);
            }
        }

        [TestMethod]
        public void FindEffectTestMethod1()
        {
            foreach (Effect effect in CardManager.Instance.Effects)
            {
                Effect tempEffect;
                bool isEffectFound = CardManager.Instance.FindEffect(effect.EffectID, out tempEffect);
                Assert.IsTrue(isEffectFound, "A valid effect (retrived from CardManager.Instance.Effects) not found by FindEffect method: " + effect.EffectID);
                Assert.IsNotNull(tempEffect, "A valid effect (retrived from CardManager.Instance.Effects) is null by FindEffect method: " + effect.EffectID);
            }
        }

        [TestMethod]
        public void FindEffectTestMethod2()
        {
            int minID = int.MaxValue;
            int maxID = int.MinValue;
            foreach (Effect effect in CardManager.Instance.Effects)
            {
                minID = Math.Min(minID, effect.EffectID);
                maxID = Math.Max(maxID, effect.EffectID);
            }

            foreach (int id in new int[] { minID - 1, maxID + 1 })
            {
                Effect tempEffect;
                bool isEffectFound = CardManager.Instance.FindEffect(id, out tempEffect);
                Assert.IsFalse(isEffectFound, "An invalid effect found by FindEffect method: " + id);
            }
        }

    }
}

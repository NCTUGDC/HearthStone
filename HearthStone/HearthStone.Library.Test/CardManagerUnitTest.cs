using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthStone.Library.Test
{
    [TestClass]
    class CardManagerUnitTest
    {

        [TestMethod]
        public void StaticInstanceTestMethod1()
        {
            CardManager instance = CardManager.Instance;
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void CardsEnumerableTestMethod1()
        {
            IEnumerable<Card> cards = CardManager.Instance.Cards;
            Assert.IsNotNull(cards);
        }


        [TestMethod]
        public void CardsEnumerableTestMethod2()
        {
            HashSet<int> IDs = new HashSet<int>();
            foreach (Card card in CardManager.Instance.Cards)
            {
                Assert.IsNotNull(card);
                Assert.IsFalse(IDs.Contains(card.CardID));
                IDs.Add(card.CardID);
            }
        }

    }
}

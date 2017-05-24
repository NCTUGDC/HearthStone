using System;
using System.Collections.Generic;
using System.Linq;
using HearthStone.Library.CardRecords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class GameCardManagerUnitTest
    {
        [TestMethod]
        public void IncreaseCoverageTestMethod1()
        {
            GameCardManager gameCardManager = new GameCardManager();
            Assert.IsNull(gameCardManager.CreateCardRecord(new TestCard()));
        }
        [TestMethod]
        public void IncreaseCoverageTestMethod2()
        {
            GameCardManager gameCardManager = new GameCardManager();
            Assert.IsNull(gameCardManager.CreareEffector(new TestEffect(1)));
        }
        [TestMethod]
        public void LoadCardTestMethod1()
        {
            GameCardManager gameCardManager = new GameCardManager();
            int addCounter = 0, updateCounter = 0;
            gameCardManager.OnCardChanged += (eventCardRecord, changeCode) => 
            {
                if (changeCode == Protocol.DataChangeCode.Add)
                    addCounter++;
                else if (changeCode == Protocol.DataChangeCode.Update)
                    updateCounter++;
            };
            gameCardManager.LoadCard(new SpellCardRecord(1, 15));
            Assert.AreEqual(1, addCounter);
            Assert.AreEqual(0, updateCounter);
            gameCardManager.LoadCard(new SpellCardRecord(1, 15));
            Assert.AreEqual(1, addCounter);
            Assert.AreEqual(1, updateCounter);
        }
        [TestMethod]
        public void LoadCardTestMethod2()
        {
            GameCardManager gameCardManager = new GameCardManager();
            gameCardManager.LoadCard(new SpellCardRecord(1, 15));
            gameCardManager.LoadCard(new SpellCardRecord(1, 15)); 
        }
    }
}

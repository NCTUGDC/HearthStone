using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class CardRecordUnitTest
    {
        class TestCardRecord : CardRecord
        {
            public TestCardRecord() : base() { }
            public TestCardRecord(int cardRecordID, int cardID) : base(cardRecordID, cardID) { }

            public void SetCardRecordID(int ID)
            {
                CardRecordID = ID;
            }
        }


        [TestMethod]
        public void CardRecordIDTestMethod1()
        {
            TestCardRecord test1 = new TestCardRecord();
            foreach (int id in new int[] { 0, 1, 2, 3, 4 }){
                test1.SetCardRecordID(id);
                Assert.IsTrue(test1.CardRecordID == id, "Invalid Setter for CardRecordID: to set " + id);
            }
        }
    }
}

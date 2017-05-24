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
            TestCardRecord(int cardRecordID, int cardID) : base(cardRecordID, cardID) { }

            void setCardRecordID(int ID)
            {
                CardRecordID = ID;
            }
        }

    }
}

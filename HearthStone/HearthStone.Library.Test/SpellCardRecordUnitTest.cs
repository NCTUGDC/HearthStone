using HearthStone.Library.CardRecords;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    [TestClass]
    public class SpellCardRecordUnitTest
    {
        [TestMethod]
        public void ConstructorUnitTest()
        {
            SpellCardRecord record = new SpellCardRecord();
            Assert.IsNotNull(record);

            record = new SpellCardRecord(1, 2);
            Assert.IsNotNull(record);
            Assert.AreEqual(-1, record.CardRecordID);
            Assert.AreEqual(2, record.CardID);

            record = new SpellCardRecord(1, 15);
            Assert.IsNotNull(record);
            Assert.AreEqual(1, record.CardRecordID);
            Assert.AreEqual(15, record.CardID);
        }
    }
}

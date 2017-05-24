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

    }
}

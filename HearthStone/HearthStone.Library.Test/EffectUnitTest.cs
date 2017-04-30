using HearthStone.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    class TestEffect : Effect
    {
        public TestEffect(int effectID) : base(effectID)
        {

        }

        public override string Description
        {
            get
            {
                return "Test Effect";
            }
        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.Test;
            }
        }
    }

    [TestClass]
    public class EffectUnitTest
    {
        [TestMethod]
        public void ConstructorTestMethod1()
        {
            Effect effect = new TestEffect(1);

            Assert.IsNotNull(effect);
            Assert.AreEqual(effect.EffectID, 1);
            Assert.AreEqual(effect.EffectType, EffectTypeCode.Test);
            Assert.AreEqual(effect.Description, "Test Effect");
        }
    }
}

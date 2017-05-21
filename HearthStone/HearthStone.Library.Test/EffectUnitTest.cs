using System;
using HearthStone.Protocol;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HearthStone.Library.Test
{
    class TestEffect : Effect
    {
        public TestEffect(int effectID) : base(effectID)
        {

        }

        public override EffectTypeCode EffectType
        {
            get
            {
                return EffectTypeCode.Test;
            }
        }

        public override Effector CreateEffector(int effectorID)
        {
            throw new NotImplementedException();
        }

        public override string Description(Game game, int selfGamePlayerID)
        {
            return "Test Effect";
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
            Assert.AreEqual(effect.Description(null, 0), "Test Effect");
        }
    }
}

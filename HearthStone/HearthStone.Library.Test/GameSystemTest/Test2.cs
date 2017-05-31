using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace HearthStone.Library.Test.GameSystemTest
{
    [TestClass]
    public class Test2
    {
        [TestMethod]
        public void Episode_CastSpell_AOE1_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"AOE1"*1 法力水晶10/10
            //玩家2場上有 手下A*1 手下B*1 手下C*1
            //輪到1號玩家 玩家1出"AOE1"
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_AOE2_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"AOE2"*1 法力水晶10/10
            //玩家2場上有 手下A*1 手下B*1 手下C*1
            //輪到1號玩家 玩家1出"AOE2"
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_AOE3_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"AOE3"*1 法力水晶10/10
            //玩家2場上有 手下A*1 手下B*1 手下C*1
            //輪到1號玩家 玩家1出"AOE3"
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_傷害A_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"傷害A"*1 法力水晶10/10
            //玩家2場上有 手下F*2
            //輪到1號玩家 玩家1出"傷害A"對位置0的手下釋放效果
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_傷害B_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"傷害B"*1 法力水晶10/10
            //玩家2場上有 手下F*2
            //輪到1號玩家 玩家1出"傷害B"對位置0的手下釋放效果
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_傷害C_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"傷害C"*1 法力水晶10/10
            //玩家2場上有 手下F*2
            //輪到1號玩家 玩家1出"傷害C"對位置1的手下釋放效果
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_傷害D_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"傷害D"*1 法力水晶10/10
            //玩家2場上有 手下F*2
            //輪到1號玩家 玩家1出"傷害D"對敵方英雄釋放效果
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            //確認敵方英雄血量狀況
            Assert.Fail();
        }
    }
}

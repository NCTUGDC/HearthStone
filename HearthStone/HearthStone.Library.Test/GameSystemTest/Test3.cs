using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace HearthStone.Library.Test.GameSystemTest
{
    [TestClass]
    public class Test3
    {
        [TestMethod]
        public void Episode_CastSpell_抽牌1_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"抽牌1"*1 牌組有CardID1~6的卡各1張 法力水晶10/10
            //輪到1號玩家 玩家1出"抽牌1"
            //確認手牌與法力水晶變化
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_抽牌2_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"抽牌2"*1 牌組有CardID1~6的卡各1張 法力水晶10/10
            //輪到1號玩家 玩家1出"抽牌2"
            //確認手牌與法力水晶變化
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_摧毀_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"摧毀"*1 法力水晶10/10
            //玩家2場上有 手下A*1 手下B*1 手下C*1
            //輪到1號玩家 玩家1出"摧毀" 對象為敵方位置1的手下
            //確認手牌與法力水晶變化
            //確認敵方場上手下的血量與生存狀況
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_生命加倍_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"生命加倍"*2 法力水晶10/10
            //玩家1場上有 手下A*1
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"生命加倍" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //玩家1出"生命加倍" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_攻擊加倍_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"攻擊加倍"*2 法力水晶10/10
            //玩家1場上有 手下A*1
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"攻擊加倍" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //玩家1出"攻擊加倍" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_手下加強A_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"手下加強A"*2 法力水晶10/10
            //玩家1場上有 手下A*1
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"手下加強A" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //玩家1出"手下加強A" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_手下加強B_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"手下加強B"*2 法力水晶10/10
            //玩家1場上有 手下A*1
            //玩家2場上有 手下A*1
            //輪到1號玩家 玩家1出"手下加強B" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //玩家1出"手下加強B" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            Assert.Fail();
        }
        [TestMethod]
        public void Episode_CastSpell_回血A_TestMethod1()
        {
            //開始一場空的遊戲 設定玩家1手牌"回血A"*4 法力水晶10/10
            //玩家1場上有 手下F*1(剩3血) 英雄血量為10
            //玩家2場上有 手下F*1(剩1血) 英雄血量為28
            //輪到1號玩家 玩家1出"回血A" 對象為我方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //確認雙方英雄的血量
            //玩家1出"回血A" 對象為敵方的手下
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //確認雙方英雄的血量
            //玩家1出"回血A" 對象為敵方的英雄
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //確認雙方英雄的血量
            //玩家1出"回血A" 對象為我方的英雄
            //確認手牌與法力水晶變化
            //確認場上手下的攻擊與血量
            //確認雙方英雄的血量
            Assert.Fail();
        }
    }
}

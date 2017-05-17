using HearthStone.Library.Cards;
using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library
{
    public class CardManager
    {
        public static CardManager Instance { get; private set; }
        static CardManager()
        {
            Instance = new CardManager();
        }

        private Dictionary<int, Card> cardDictionary = new Dictionary<int, Card>();
        public IEnumerable<Card> Cards { get { return cardDictionary.Values; } }

        private CardManager()
        {
            LoadDefaultCards();
        }
        public void LoadDefaultCards()
        {
            List<Card> cardSet = new List<Card>
            {
                new ServantCard(1, 4, "冰風雪人", new List<Effect>(), 4, 5, RarityCode.Free),
                new SpellCard(2, 4, "奉獻", new List<Effect>(), RarityCode.Free),
                new SpellCard(3, 2, "神聖精神", new List<Effect>(), RarityCode.Free),
                new ServantCard(4, 6, "恐怖的煉獄火", new List<Effect>(), 6, 6, RarityCode.Free),
                new SpellCard(5, 2, "斬殺", new List<Effect>(), RarityCode.Free),
                new SpellCard(6, 3, "暗言術：死", new List<Effect>(), RarityCode.Free),
                new SpellCard(7, 3, "飛舞刀刃", new List<Effect>(), RarityCode.Free),
                new WeaponCard(8, 2, "熾炎戰斧", new List<Effect>(), 3, 2, RarityCode.Free),
                new ServantCard(9, 6, "火元素", new List<Effect>(), 6, 5, RarityCode.Free),
                new SpellCard(10, 4, "火球術", new List<Effect>(), RarityCode.Free),
                new SpellCard(11, 7, "烈焰風暴", new List<Effect>(), RarityCode.Free),
                new ServantCard(12, 4, "森金禦盾大師", new List<Effect>(), 3, 5, RarityCode.Free),
                new ServantCard(13, 1, "石牙野豬", new List<Effect>(), 1, 1, RarityCode.Free),
                new ServantCard(14, 4, "真銀勇士劍", new List<Effect>(), 4, 2, RarityCode.Free),
                new ServantCard(15, 5, "利爪德魯伊", new List<Effect>(), 4, 4, RarityCode.Common),
                new ServantCard(16, 2, "掠寶囤積者", new List<Effect>(), 2, 1, RarityCode.Common),
                new ServantCard(17, 3, "血色十字軍", new List<Effect>(), 3, 1, RarityCode.Common),
                new ServantCard(18, 2, "巫士的學徒", new List<Effect>(), 3, 2, RarityCode.Common),
                new SpellCard(19, 3, "放狗", new List<Effect>(), RarityCode.Common),
                new ServantCard(20, 3, "冷光神諭者", new List<Effect>(), 2, 2, RarityCode.Rare),
                new ServantCard(21, 4, "阿古斯防衛者", new List<Effect>(), 2, 3, RarityCode.Rare),
                new WeaponCard(22, 3, "鷹角弓", new List<Effect>(), 3, 2, RarityCode.Rare),
                new ServantCard(23, 6, "加基森拍賣師", new List<Effect>(), 4, 4, RarityCode.Rare),
                new ServantCard(24, 3, "受傷的大劍師", new List<Effect>(), 4, 7, RarityCode.Rare),
                new ServantCard(25, 6, "綁匪", new List<Effect>(), 5, 3, RarityCode.Epic),
                new SpellCard(26, 10, "炎爆術", new List<Effect>(), RarityCode.Epic),
                new WeaponCard(27, 3, "正義之劍", new List<Effect>(), 1, 5, RarityCode.Epic),
                new ServantCard(28, 10, "死亡之翼", new List<Effect>(), 12, 12, RarityCode.Legendary),
            };

            cardSet.ForEach(x => cardDictionary.Add(x.CardID, x));
        }
        public bool FindCard(int cardID, out Card card)
        {
            if(cardDictionary.ContainsKey(cardID))
            {
                card = cardDictionary[cardID];
                return true;
            }
            else
            {
                card = null;
                return false;
            }
        }
    }
}

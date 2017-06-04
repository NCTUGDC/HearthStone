using HearthStone.Library.Cards;
using HearthStone.Library.Effects;
using HearthStone.Protocol;
using System.Collections.Generic;
using System.Linq;

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

        private Dictionary<int, Effect> effectDictionary = new Dictionary<int, Effect>();
        public IEnumerable<Effect> Effects { get { return effectDictionary.Values; } }

        private CardManager()
        {
            LoadDefaultCards();
        }
        public void LoadDefaultCards()
        {
            Dictionary<string, Effect> effectSet = new Dictionary<string, Effect>
            {
                { "衝鋒", new ChargeEffect(1) },
                { "嘲諷", new TauntEffect(2) },
                { "風怒", new WindfuryEffect(3) },
                { "沉默", new SilenceMinionEffect(4) },
                { "法傷+1", new SpellDamageEffect(5, 1) },
                { "法傷+2", new SpellDamageEffect(6, 2) },
                { "對全部敵方手下造成1點傷害", new DealSpellDamageToAllEnemyMinionsEffect(7, 1) },
                { "對全部敵方手下造成2點傷害", new DealSpellDamageToAllEnemyMinionsEffect(8, 2) },
                { "對全部敵方手下造成4點傷害", new DealSpellDamageToAllEnemyMinionsEffect(9, 4) },
                { "抽2張牌", new DrawCardEffect(10, 2) },
                { "抽3張牌", new DrawCardEffect(11, 3) },
                { "造成2點傷害", new DealSpellDamageEffect(12, 2) },
                { "造成3點傷害", new DealSpellDamageEffect(13, 3) },
                { "造成6點傷害", new DealSpellDamageEffect(14, 6) },
                { "造成10點傷害", new DealSpellDamageEffect(15, 10) },
                { "摧毀一個敵方手下", new DestroyEnemyMinionEffect(16) },
                { "使一個手下的生命值加倍", new DoubleMinionHealthEffect(17) },
                { "使一個手下的攻擊力加倍", new DoubleMinionAttackEffect(18) },
                { "賦予一個手下攻擊+2", new GiveMinionAttackBuffEffect(19, 2) },
                { "賦予一個手下生命+2", new GiveMinionHealthBuffEffect(20, 2) },
                { "賦予一個手下攻擊+4", new GiveMinionAttackBuffEffect(21, 4) },
                { "賦予一個手下生命+4", new GiveMinionHealthBuffEffect(22, 4) },
                { "恢復4點血量", new RestoreHealthEffect(23, 4) },
            };
            List<Card> cardSet = new List<Card>
            {
                new ServantCard(1, 1, "手下A", new List<Effect>(), 1, 2, RarityCode.Free),
                new ServantCard(2, 2, "手下B", new List<Effect>(), 2, 3, RarityCode.Free),
                new ServantCard(3, 3, "手下C", new List<Effect>(), 3, 4, RarityCode.Free),
                new ServantCard(4, 4, "手下D", new List<Effect>(), 4, 5, RarityCode.Free),
                new ServantCard(5, 5, "手下E", new List<Effect>(), 5, 6, RarityCode.Free),
                new ServantCard(6, 6, "手下F", new List<Effect>(), 6, 7, RarityCode.Free),

                new ServantCard(7, 1, "衝鋒手下", new List<Effect> { effectSet["衝鋒"] }, 1, 1, RarityCode.Common),
                new ServantCard(8, 2, "嘲諷手下", new List<Effect> { effectSet["嘲諷"] }, 1, 2, RarityCode.Common),
                new ServantCard(9, 3, "風怒手下", new List<Effect> { effectSet["風怒"] }, 1, 2, RarityCode.Common),
                new ServantCard(10, 4, "沉默手下", new List<Effect> { effectSet["沉默"] }, 1, 2, RarityCode.Common),
                new ServantCard(11, 5, "法傷手下", new List<Effect> { effectSet["法傷+1"] }, 1, 2, RarityCode.Common),

                new WeaponCard(12, 2, "精良武器", new List<Effect>(), 2, 3, RarityCode.Rare),
                new WeaponCard(13, 6, "風怒武器", new List<Effect> { effectSet["風怒"] }, 2, 8, RarityCode.Epic),
                new WeaponCard(14, 8, "法傷武器", new List<Effect> { effectSet["法傷+2"] }, 5, 2, RarityCode.Legendary),

                new SpellCard(15, 2, "AOE1", new List<Effect> { effectSet["對全部敵方手下造成1點傷害"] }, RarityCode.Free),
                new SpellCard(16, 4, "AOE2", new List<Effect> { effectSet["對全部敵方手下造成2點傷害"] }, RarityCode.Free),
                new SpellCard(17, 7, "AOE3", new List<Effect> { effectSet["對全部敵方手下造成4點傷害"] }, RarityCode.Free),
                new SpellCard(18, 3, "抽牌1", new List<Effect> { effectSet["抽2張牌"] }, RarityCode.Free),
                new SpellCard(19, 5, "抽牌2", new List<Effect> { effectSet["抽3張牌"] }, RarityCode.Rare),
                new SpellCard(20, 1, "傷害A", new List<Effect> { effectSet["造成2點傷害"] }, RarityCode.Free),
                new SpellCard(21, 2, "傷害B", new List<Effect> { effectSet["造成3點傷害"] }, RarityCode.Free),
                new SpellCard(22, 4, "傷害C", new List<Effect> { effectSet["造成6點傷害"] }, RarityCode.Common),
                new SpellCard(23, 10, "傷害D", new List<Effect> { effectSet["造成10點傷害"] }, RarityCode.Epic),
                new SpellCard(24, 5, "摧毀", new List<Effect> { effectSet["摧毀一個敵方手下"] }, RarityCode.Free),
                new SpellCard(25, 5, "攻擊加倍", new List<Effect> { effectSet["使一個手下的攻擊力加倍"] }, RarityCode.Rare),
                new SpellCard(26, 2, "生命加倍", new List<Effect> { effectSet["使一個手下的生命值加倍"] }, RarityCode.Common),
                new SpellCard(27, 2, "手下加強A", new List<Effect> { effectSet["賦予一個手下攻擊+2"], effectSet["賦予一個手下生命+2"] }, RarityCode.Rare),
                new SpellCard(28, 4, "手下加強B", new List<Effect> { effectSet["賦予一個手下攻擊+4"], effectSet["賦予一個手下生命+4"] }, RarityCode.Free),
                new SpellCard(29, 1, "回血A", new List<Effect> { effectSet["恢復4點血量"] }, RarityCode.Free),
            };
            cardSet.ForEach(x => cardDictionary.Add(x.CardID, x));
            effectSet.Values.ToList().ForEach(x => effectDictionary.Add(x.EffectID, x));
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
        public bool FindEffect(int effectID, out Effect effect)
        {
            if (effectDictionary.ContainsKey(effectID))
            {
                effect = effectDictionary[effectID];
                return true;
            }
            else
            {
                effect = null;
                return false;
            }
        }
    }
}

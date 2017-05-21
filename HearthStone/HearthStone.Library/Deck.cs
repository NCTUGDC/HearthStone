using HearthStone.Protocol;
using System.Collections.Generic;
using System.Linq;

namespace HearthStone.Library
{
    public class Deck
    {
        public int DeckID { get; private set; }
        public string DeckName { get; private set; }
        public int MaxCardCount { get; private set; }
        private List<Card> cards;
        public IEnumerable<Card> Cards { get { return cards; } }
        public int TotalCardCount { get { return cards.Count; } }
        public bool IsCompleted { get { return TotalCardCount == MaxCardCount; } }

        public delegate void DeckCardChangedEventHandler(Card card, DataChangeCode changeCode);
        public event DeckCardChangedEventHandler OnCardChanged;

        public Deck(int deckID, string deckName, int maxCardCount)
        {
            DeckID = deckID;
            DeckName = deckName;
            MaxCardCount = maxCardCount;
            cards = new List<Card>();
        }
        public Deck(int deckID, string deckName, int maxCardCount, List<Card> cards)
        {
            DeckID = deckID;
            DeckName = deckName;
            MaxCardCount = maxCardCount;
            this.cards = cards;
        }

        public bool AddCard(Card card)
        {
            if (card == null || TotalCardCount >= MaxCardCount)
                return false;
            else if(card.Rarity == Protocol.RarityCode.Legendary)
            {
                if (CardCount(card.CardID) >= 1)
                {
                    return false;
                }
                else
                {
                    cards.Add(card);
                    OnCardChanged?.Invoke(card, DataChangeCode.Add);
                    return true;
                }
            }
            else
            {
                if (CardCount(card.CardID) >= 2)
                {
                    return false;
                }
                else
                {
                    cards.Add(card);
                    OnCardChanged?.Invoke(card, DataChangeCode.Add);
                    return true;
                }
            }
        }
        public bool RemoveCard(int cardID)
        {
            if(CardCount(cardID) > 0)
            {
                int index = cards.FindIndex(x => x.CardID == cardID);
                Card card = cards[index];
                cards.RemoveAt(index);
                OnCardChanged?.Invoke(card, DataChangeCode.Remove);
                return true;
            }
            else
            {
                return false;
            }
        }
        public int CardCount(int cardID)
        {
            return cards.Count(x => x.CardID == cardID);
        }
    }
}

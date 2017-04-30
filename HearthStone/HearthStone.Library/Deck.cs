using System;
using System.Collections.Generic;

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
            throw new NotImplementedException("Deck AddCard");
        }
        public bool RemoveCard(int cardID)
        {
            throw new NotImplementedException("Deck RemoveCard");
        }
        public int CardCount(int cardID)
        {
            throw new NotImplementedException("Deck ContainsCrad");
        }
    }
}

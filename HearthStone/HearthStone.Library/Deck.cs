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
        public IEnumerable<Card> Cards { get { throw new NotImplementedException("Deck Cards"); } }
        public bool IsCompleted { get { throw new NotImplementedException("Deck IsCompleted"); } }

        public Deck(int deckID, string deckName, int maxCardCount)
        {
            throw new NotImplementedException("Deck Constructor");
        }
        public Deck(int deckID, string deckName, int maxCardCount, List<Card> cards)
        {
            throw new NotImplementedException("Deck Constructor");
        }

        public bool AddCard(Card card)
        {
            throw new NotImplementedException("Deck AddCard");
        }
        public bool RemoveCard(int cardID)
        {
            throw new NotImplementedException("Deck RemoveCard");
        }
        public int CradCount(int cardID)
        {
            throw new NotImplementedException("Deck ContainsCrad");
        }
    }
}

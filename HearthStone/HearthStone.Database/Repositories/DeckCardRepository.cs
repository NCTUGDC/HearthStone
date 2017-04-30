using HearthStone.Library;
using System.Collections.Generic;

namespace HearthStone.Database.Repositories
{
    public abstract class DeckCardRepository
    {
        public abstract bool Create(int deckID, int cardID);
        public abstract bool Delete(int deckCardID);
        public abstract List<Card> ListOfDeck(int deckID);
    }
}

using HearthStone.Library;
using System.Collections.Generic;

namespace HearthStone.Database.Repositories
{
    public abstract class DeckRepository
    {
        public abstract bool Create(int ownerPlayerID, string deckName, out Deck deck);
        public abstract bool Delete(int deckID);
        public abstract List<Deck> ListOfPlayer(int playerID);
    }
}

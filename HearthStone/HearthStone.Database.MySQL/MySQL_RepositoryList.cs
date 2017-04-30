using HearthStone.Database.MySQL.Repositories;
using HearthStone.Database.Repositories;

namespace HearthStone.Database.MySQL
{
    class MySQL_RepositoryList : RepositoryList
    {
        private MySQL_PlayerRepository playerRepository = new MySQL_PlayerRepository();
        public override PlayerRepository PlayerRepository { get { return playerRepository; } }

        private MySQL_DeckRepository deckRepository = new MySQL_DeckRepository();
        public override DeckRepository DeckRepository { get { return deckRepository; } }

        private MySQL_DeckCardRepository deckCardRepository = new MySQL_DeckCardRepository();
        public override DeckCardRepository DeckCardRepository { get { return deckCardRepository; } }
    }
}

using HearthStone.Database.MySQL.Repositories;
using HearthStone.Database.Repositories;

namespace HearthStone.Database.MySQL
{
    class MySQL_RepositoryList : RepositoryList
    {
        private MySQL_PlayerRepository playerRepository = new MySQL_PlayerRepository();
        public override PlayerRepository PlayerRepository
        {
            get
            {
                return playerRepository;
            }
        }
    }
}

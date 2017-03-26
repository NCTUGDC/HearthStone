using HearthStone.Database.Repositories;

namespace HearthStone.Database
{
    public abstract class RepositoryList
    {
        #region player data
        public abstract PlayerRepository PlayerRepository { get; }
        #endregion
    }
}

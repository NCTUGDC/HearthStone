namespace HearthStone.Database
{
    public abstract class DatabaseService
    {
        protected static DatabaseService instance;
        
        public static void Initial(DatabaseService instance)
        {
            DatabaseService.instance = instance;
        }

        public static ConnectionList ConnectionList { get { return instance?.InternalConnectionList; } }
        public static RepositoryList RepositoryList { get { return instance?.InternalRepositoryList; } }

        protected abstract ConnectionList InternalConnectionList { get; }
        protected abstract RepositoryList InternalRepositoryList { get; }

        public static string DatabaseName { get; private set; }

        public static bool Connect(string hostName, string userName, string password, string database)
        {
            DatabaseName = database;
            ConnectionList.Connect(hostName, userName, password, database);
            return ConnectionList.Connected;
        }

        public static void Dispose()
        {
            ConnectionList?.Dispose();
        }
    }
}

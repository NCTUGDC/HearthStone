using HearthStone.Database.Connections;
using MySql.Data.MySqlClient;

namespace HearthStone.Database.MySQL.Connections
{
    class MySQL_PlayerDataConnection : PlayerDataConnection
    {
        protected override string DatabaseName
        {
            get
            {
                return "PlayerData";
            }
        }

        public override bool Connect(string hostName, string userName, string password, string database)
        {
            string connectString = $"server={hostName};uid={userName};pwd={password};database={database}_{DatabaseName}";
            connection = new MySqlConnection(connectString);

            childConnections.ForEach(x => x.Connect(hostName, userName, password, $"{database}_{DatabaseName}"));

            return Connected;
        }
    }
}

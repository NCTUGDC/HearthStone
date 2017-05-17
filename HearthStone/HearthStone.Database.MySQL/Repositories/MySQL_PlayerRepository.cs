using HearthStone.Database.Repositories;
using HearthStone.Library;
using MySql.Data.MySqlClient;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace HearthStone.Database.MySQL.Repositories
{
    class MySQL_PlayerRepository : PlayerRepository
    {
        public override bool Contains(string account, out int playerID)
        {
            lock(DatabaseService.ConnectionList.PlayerDataConnection)
            {
                using (MySqlCommand command = new MySqlCommand("SELECT PlayerID FROM PlayerCollection WHERE Account = @account;", DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("account", account);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            playerID = reader.GetInt32(0);
                            return true;
                        }
                        else
                        {
                            playerID = -1;
                            return false;
                        }
                    }
                }
            }
        }

        public override bool Read(int playerID, out Player player)
        {
            string sqlString = @"SELECT 
                LastConnectedIPAddress, Account, Nickname
                from PlayerCollection WHERE PlayerID = @playerID;";
            lock(DatabaseService.ConnectionList.PlayerDataConnection)
            {
                using (MySqlCommand command = new MySqlCommand(sqlString, DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("playerID", playerID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            IPAddress lastConnectedIPAddress = IPAddress.Parse(reader.GetString(0));
                            string account = reader.GetString(1);
                            string nickname = reader.GetString(2);

                            player = new Player(playerID, lastConnectedIPAddress, account, nickname);
                            return true;
                        }
                        else
                        {
                            player = null;
                            return false;
                        }
                    }
                }
            }
        }

        public override bool Register(IPAddress lastConnectedIPAddress, string account, string passwordHash, string nickname)
        {
            int playerID;
            if (Contains(account, out playerID))
            {
                return false;
            }
            else
            {
                string sqlString = @"INSERT INTO PlayerCollection 
                    (LastConnectedIPAddress, Account, PasswordHash, Nickname) VALUES (@lastConnectedIPAddress, @account, @passwordHash, @nickname) ;";
                lock (DatabaseService.ConnectionList.PlayerDataConnection)
                {
                    using (MySqlCommand command = new MySqlCommand(sqlString, DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                    {
                        command.Parameters.AddWithValue("lastConnectedIPAddress", lastConnectedIPAddress.ToString());
                        command.Parameters.AddWithValue("account", account);
                        command.Parameters.AddWithValue("passwordHash", passwordHash);
                        command.Parameters.AddWithValue("nickname", nickname);

                        if(command.ExecuteNonQuery() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        public override void Save(Player player)
        {
            throw new NotImplementedException();
        }
        public override bool LoginCheck(string account, string password)
        {
            lock (DatabaseService.ConnectionList.PlayerDataConnection)
            {
                using (MySqlCommand command = new MySqlCommand("SELECT 1 FROM PlayerCollection WHERE Account = @account and PasswordHash = @passwordHash;", DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    SHA512 sha512 = new SHA512CryptoServiceProvider();
                    string passwordHash = Convert.ToBase64String(sha512.ComputeHash(Encoding.Default.GetBytes(password)));

                    command.Parameters.AddWithValue("@account", account);
                    command.Parameters.AddWithValue("@passwordHash", passwordHash);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }
    }
}

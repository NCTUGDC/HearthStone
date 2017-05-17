using HearthStone.Database.Repositories;
using HearthStone.Library;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HearthStone.Database.MySQL.Repositories
{
    class MySQL_DeckRepository : DeckRepository
    {
        public override bool Create(int ownerPlayerID, string deckName, out Deck deck)
        {
            string sqlString = @"INSERT INTO DeckCollection 
                (OwnerPlayerID, DeckName, MaxCardCount) VALUES (@ownerPlayerID, @deckName, 10) ;
                SELECT LAST_INSERT_ID();";
            lock (DatabaseService.ConnectionList.PlayerDataConnection)
            {
                using (MySqlCommand command = new MySqlCommand(sqlString, DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("ownerPlayerID", ownerPlayerID);
                    command.Parameters.AddWithValue("deckName", deckName);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int deckID = reader.GetInt32(0);
                            deck = new Deck(deckID, deckName, 10);
                            return true;
                        }
                        else
                        {
                            deck = null;
                            return false;
                        }
                    }
                }
            }
        }

        public override bool Delete(int deckID)
        {
            string sqlString = @"DELETE FROM DeckCollection 
                WHERE DeckID = @deckID;";
            lock (DatabaseService.ConnectionList.PlayerDataConnection)
                using (MySqlCommand command = new MySqlCommand(sqlString, DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("deckID", deckID);
                    return command.ExecuteNonQuery() > 0;
                }
        }

        public override List<Deck> ListOfPlayer(int playerID)
        {
            List<Deck> decks = new List<Deck>();
            string sqlString = @"SELECT DeckID, DeckName, MaxCardCount from DeckCollection 
                WHERE OwnerPlayerID = @playerID;";
            lock (DatabaseService.ConnectionList.PlayerDataConnection)
                using (MySqlCommand command = new MySqlCommand(sqlString, DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("playerID", playerID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int deckID = reader.GetInt32(0);
                            string deckName = reader.GetString(1);
                            int maxCardCount = reader.GetInt32(2);
                            decks.Add(new Deck(deckID, deckName, maxCardCount));
                        }
                    }
                }

            foreach(var deck in decks)
            {
                foreach(var card in DatabaseService.RepositoryList.DeckCardRepository.ListOfDeck(deck.DeckID))
                {
                    deck.AddCard(card);
                }
            }

            return decks;
        }
    }
}

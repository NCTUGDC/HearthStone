using HearthStone.Database.Repositories;
using HearthStone.Library;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace HearthStone.Database.MySQL.Repositories
{
    class MySQL_DeckCardRepository : DeckCardRepository
    {
        public override bool Create(int deckID, int cardID)
        {
            string sqlString = @"INSERT INTO DeckCardCollection 
                    (DeckID, CardID) VALUES (@deckID, @cardID) ;";
            lock (DatabaseService.ConnectionList.PlayerDataConnection)
            {
                using (MySqlCommand command = new MySqlCommand(sqlString, DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("deckID", deckID);
                    command.Parameters.AddWithValue("cardID", cardID);
                    if (command.ExecuteNonQuery() == 1)
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

        public override bool Delete(int deckCardID)
        {
            string sqlString = @"DELETE FROM DeckCardCollection 
                WHERE DeckCardID = @deckCardID;";
            lock (DatabaseService.ConnectionList.PlayerDataConnection)
                using (MySqlCommand command = new MySqlCommand(sqlString, DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("deckCardID", deckCardID);
                    return command.ExecuteNonQuery() > 0;
                }
        }

        public override List<Card> ListOfDeck(int deckID)
        {
            List<Card> cards = new List<Card>();
            string sqlString = @"SELECT CardID from DeckCardCollection 
                WHERE DeckID = @deckID;";
            lock (DatabaseService.ConnectionList.PlayerDataConnection)
                using (MySqlCommand command = new MySqlCommand(sqlString, DatabaseService.ConnectionList.PlayerDataConnection.Connection as MySqlConnection))
                {
                    command.Parameters.AddWithValue("deckID", deckID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int cardID = reader.GetInt32(0);
                            Card card;
                            if(CardManager.Instance.FindCard(cardID, out card))
                            {
                                cards.Add(card);
                            }
                        }
                    }
                }
            return cards;
        }
    }
}

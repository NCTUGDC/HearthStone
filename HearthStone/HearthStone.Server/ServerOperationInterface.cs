﻿using HearthStone.Database;
using HearthStone.Library;
using HearthStone.Protocol;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace HearthStone.Server
{
    class ServerOperationInterface : OperationInterface
    {
        public bool AddCardToDeck(int deckID, int cardID, out ReturnCode returnCode, out string errorMessage)
        {
            if (DatabaseService.RepositoryList.DeckCardRepository.Create(deckID, cardID))
            {
                returnCode = ReturnCode.Correct;
                errorMessage = "";
                return true;
            }
            else
            {
                returnCode = ReturnCode.DatabaseError;
                errorMessage = "Database AddCardToDeck Fail";
                return false;
            }
        }

        public bool CreateDeck(int playerID, string deckName, out ReturnCode returnCode, out string errorMessage, out Deck deck)
        {
            if(DatabaseService.RepositoryList.DeckRepository.Create(playerID, deckName, out deck))
            {
                returnCode = ReturnCode.Correct;
                errorMessage = "";
                return true;
            }
            else
            {
                returnCode = ReturnCode.DatabaseError;
                errorMessage = "Database Create Deck Fail";
                return false;
            }
        }

        public bool DeleteDeck(int deckID, out ReturnCode returnCode, out string errorMessage)
        {
            if (DatabaseService.RepositoryList.DeckRepository.Delete(deckID))
            {
                returnCode = ReturnCode.Correct;
                errorMessage = "";
                return true;
            }
            else
            {
                returnCode = ReturnCode.DatabaseError;
                errorMessage = "Database Delete Deck Fail";
                return false;
            }
        }

        public void FindOpponent(Player player, Deck deck)
        {
            PlayerMatchManager.Instance.AddPlayer(player, deck);
        }

        public bool Login(string account, string password, out ReturnCode returnCode, out string errorMessage, out Player player)
        {
            if(DatabaseService.RepositoryList.PlayerRepository.LoginCheck(account, password))
            {
                int playerID;
                if(DatabaseService.RepositoryList.PlayerRepository.Contains(account, out playerID) &&
                   DatabaseService.RepositoryList.PlayerRepository.Read(playerID, out player))
                {
                    returnCode = ReturnCode.Correct;
                    errorMessage = "";
                    AssemblyPlayer(player);
                    return true;
                }
                else
                {
                    returnCode = ReturnCode.NotExisted;
                    errorMessage = "cannot find player";
                    player = null;
                    return false;
                }
            }
            else
            {
                returnCode = ReturnCode.PermissionDeny;
                errorMessage = "account or password incorrect";
                player = null;
                return false;
            }
        }

        public bool Register(IPAddress lastConnectedIPAddress, string account, string password, string nickname, out ReturnCode returnCode, out string errorMessage)
        {
            int playerID;
            if(DatabaseService.RepositoryList.PlayerRepository.Contains(account, out playerID))
            {
                returnCode = ReturnCode.AlreadyExisted;
                errorMessage = "account has been used";
                return false;
            }
            else
            {
                if(account.Length < 4 || password.Length < 4 || nickname.Length < 4)
                {
                    returnCode = ReturnCode.InvalidParameter;
                    errorMessage = "account or password or nickname is too short";
                    return false;
                }
                else
                {
                    SHA512 sha512 = new SHA512CryptoServiceProvider();
                    string passwordHash = Convert.ToBase64String(sha512.ComputeHash(Encoding.Default.GetBytes(password)));
                    DatabaseService.RepositoryList.PlayerRepository.Register(lastConnectedIPAddress, account, passwordHash, nickname);
                    returnCode = ReturnCode.Correct;
                    errorMessage = "";
                    return true;
                }
            }
        }

        private void AssemblyPlayer(Player player)
        {
            player.OnDeckChanged += (deck, changeCode) => 
            {
                if(changeCode == DataChangeCode.Add)
                {
                    deck.OnCardChanged += (eventDeck, card, cardChangeCode) => 
                    {
                        switch(cardChangeCode)
                        {
                            case DataChangeCode.Add:
                                DatabaseService.RepositoryList.DeckCardRepository.Create(eventDeck.DeckID, card.CardID);
                                break;
                            case DataChangeCode.Remove:
                                DatabaseService.RepositoryList.DeckCardRepository.Delete(eventDeck.DeckID, card.CardID);
                                break;
                        }
                        player.EventManager.SyncDataBroker.SyncDeckCardChanged(eventDeck.DeckID, card.CardID, cardChangeCode);
                    };
                }
            };
            DatabaseService.RepositoryList.DeckRepository.ListOfPlayer(player.PlayerID).ForEach(x => player.LoadDeck(x));
            player.OnDeckChanged += player.EventManager.SyncDataBroker.SyncDeckChanged;
        }
    }
}

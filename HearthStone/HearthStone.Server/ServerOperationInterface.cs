using HearthStone.Database;
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
                    //save player 
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
    }
}

using HearthStone.Protocol;
using System.Net;

namespace HearthStone.Library
{
    public interface OperationInterface
    {
        bool Register(IPAddress lastConnectedIPAddress, string account, string password, string nickname, out ReturnCode returnCode, out string errorMessage);
        bool Login(string account, string password, out ReturnCode returnCode, out string errorMessage, out Player player);
    }
}

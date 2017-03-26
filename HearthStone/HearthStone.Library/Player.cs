using HearthStone.Library.CommunicationInfrastructure.Event.Managers;
using HearthStone.Library.CommunicationInfrastructure.Operation.Managers;
using HearthStone.Library.CommunicationInfrastructure.Response.Managers;
using System.Net;

namespace HearthStone.Library
{
    public class Player
    {
        public EndPoint EndPoint { get; private set; }
        public int PlayerID { get; private set; }
        public IPAddress LastConnectedIPAddress { get; set; }
        public string Account { get; private set; }
        public string Nickname { get; private set; }

        public PlayerEventManager EventManager { get; private set; }
        public PlayerOperationManager OperationManager { get; private set; }
        public PlayerResponseManager ResponseManager { get; private set; }


        public Player(int playerID, IPAddress lastConnectedIPAddress, string account, string nickname)
        {
            PlayerID = playerID;
            LastConnectedIPAddress = lastConnectedIPAddress;
            Account = account;
            Nickname = nickname;

            EventManager = new PlayerEventManager(this);
            OperationManager = new PlayerOperationManager(this);
            ResponseManager = new PlayerResponseManager(this);
        }
        public override string ToString()
        {
            return $"PlayerID: {PlayerID}";
        }
    }
}

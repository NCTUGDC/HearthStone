using System.Net;
using HearthStone.Library.CommunicationInfrastructure;
using HearthStone.Library.CommunicationInfrastructure.Event.Managers;
using HearthStone.Library.CommunicationInfrastructure.Operation.Managers;
using HearthStone.Library.CommunicationInfrastructure.Response.Managers;

namespace HearthStone.Library
{
    public class EndPoint
    {
        public Player Player { get; protected set; }
        public bool IsOnline { get { return Player != null; } }

        private IPAddress lastConnectedIPAddress;
        public IPAddress LastConnectedIPAddress
        {
            get
            {
                return (IsOnline) ? Player.LastConnectedIPAddress : lastConnectedIPAddress;
            }
            protected set
            {
                lastConnectedIPAddress = value;
                if (IsOnline)
                {
                    Player.LastConnectedIPAddress = value;
                }
            }
        }

        public CommunicationInterface CommunicationInterface { get; private set; }
        public EndPointEventManager EventManager { get; private set; }
        public EndPointOperationManager OperationManager { get; private set; }
        public EndPointResponseManager ResponseManager { get; private set; }

        public EndPoint(CommunicationInterface communicationInterface)
        {
            CommunicationInterface = communicationInterface;
            EventManager = new EndPointEventManager(this);
            OperationManager = new EndPointOperationManager(this);
            ResponseManager = new EndPointResponseManager(this);
        }
    }
}

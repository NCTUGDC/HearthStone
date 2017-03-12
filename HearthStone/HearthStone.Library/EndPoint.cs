using System.Net;

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
    }
}

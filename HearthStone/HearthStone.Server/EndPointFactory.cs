using System;
using System.Collections.Generic;
using System.Linq;
using HearthStone.Library;
using System.Threading.Tasks;

namespace HearthStone.Server
{
    public class EndPointFactory
    {
        public static EndPointFactory Instance { get; private set; }

        public static void Initial()
        {
            Instance = new EndPointFactory();
        }

        private Dictionary<Guid, ServerEndPoint> connectedEndPoints = new Dictionary<Guid, ServerEndPoint>();
        public IEnumerable<ServerEndPoint> EndPoints { get { return connectedEndPoints.Values.ToArray(); } }

        public bool ContainsEndPointGuid(Guid guid)
        {
            return connectedEndPoints.ContainsKey(guid);
        }
        public bool EndPointConnect(ServerEndPoint endPoint)
        {
            if (ContainsEndPointGuid(endPoint.Guid))
            {
                LogService.InfoFormat($"EndPoint Guid: {endPoint.Guid} Duplicated Connect from {endPoint.LastConnectedIPAddress}");
                return false;
            }
            else
            {
                connectedEndPoints.Add(endPoint.Guid, endPoint);
                LogService.InfoFormat($"EndPoint Guid: {endPoint.Guid} Connect from {endPoint.LastConnectedIPAddress}");
                return true;
            }
        }
        public void EndPointDisconnect(ServerEndPoint endPoint)
        {
            if (ContainsEndPointGuid(endPoint.Guid))
            {
                connectedEndPoints.Remove(endPoint.Guid);
                LogService.InfoFormat($"EndPoint Guid: {endPoint.Guid} Connect from {endPoint.LastConnectedIPAddress}");
            }
        }
    }
}

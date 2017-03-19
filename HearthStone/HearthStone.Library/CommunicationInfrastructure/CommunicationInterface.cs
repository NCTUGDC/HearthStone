using HearthStone.Protocol;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure
{
    public abstract class CommunicationInterface
    {
        public EndPoint EndPoint { get; private set; }
        public virtual void BindEndPoint(EndPoint endPoint)
        {
            EndPoint = endPoint;
        }
        public abstract void SendEvent(EndPointEventCode eventCode, Dictionary<byte, object> parameters);
        public abstract void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters);
        public abstract void SendResponse(EndPointOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters);
    }
}

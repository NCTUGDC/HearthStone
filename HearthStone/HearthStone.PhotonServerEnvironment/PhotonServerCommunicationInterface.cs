using HearthStone.Library;
using HearthStone.Library.CommunicationInfrastructure;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.OperationCodes;
using Photon.SocketServer;
using System.Collections.Generic;

namespace HearthStone.PhotonServerEnvironment
{
    class PhotonServerCommunicationInterface : CommunicationInterface
    {
        private PhotonServerPeer peer;

        public PhotonServerCommunicationInterface(PhotonServerPeer peer)
        {
            this.peer = peer;
        }

        public override void SendEvent(EndPointEventCode eventCode, Dictionary<byte, object> parameters)
        {
            EventData eventData = new EventData
            {
                Code = (byte)eventCode,
                Parameters = parameters
            };
            peer.SendEvent(eventData, new SendParameters());
        }

        public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            LogService.Fatal($"PhotonServer SendOperation from : {peer.ServerEndPoint.LastConnectedIPAddress}");
        }

        public override void SendResponse(EndPointOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            OperationResponse response = new OperationResponse((byte)operationCode, parameters)
            {
                ReturnCode = (short)returnCode,
                DebugMessage = debugMessage
            };
            peer.SendOperationResponse(response, new SendParameters());
        }
    }
}

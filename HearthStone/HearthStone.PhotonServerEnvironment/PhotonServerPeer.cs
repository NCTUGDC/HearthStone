using HearthStone.Library;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Server;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System;
using System.Collections.Generic;

namespace HearthStone.PhotonServerEnvironment
{
    public class PhotonServerPeer : ClientPeer
    {
        public ServerEndPoint ServerEndPoint { get; private set; }
        public Guid Guid { get { return ServerEndPoint.Guid; } }

        public PhotonServerPeer(InitRequest initRequest) : base(initRequest)
        {
            ServerEndPoint = new ServerEndPoint(new PhotonServerCommunicationInterface(this));
            EndPointFactory.Instance.EndPointConnect(ServerEndPoint);
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            EndPointFactory.Instance.EndPointDisconnect(ServerEndPoint);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            EndPointOperationCode operationCode = (EndPointOperationCode)operationRequest.OperationCode;
            Dictionary<byte, object> parameters = operationRequest.Parameters;

            string errorMessage;
            if(!ServerEndPoint.OperationManager.Operate(operationCode, parameters, out errorMessage))
            {
                LogService.Error($"OperationRequest Fail, Guid: {Guid}\nErrorMessage: {errorMessage}");
            }
        }
    }
}

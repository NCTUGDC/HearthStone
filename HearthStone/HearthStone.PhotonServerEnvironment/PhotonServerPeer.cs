﻿using System;
using System.Collections.Generic;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using HearthStone.Server;
using HearthStone.Protocol.Communication.OperationCodes;

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
            ServerEndPoint.OperationManager.Operate(operationCode, parameters);
        }
    }
}

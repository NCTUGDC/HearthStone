using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using UnityEngine;
using HearthStone.Library;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.EventCodes;

public class PhotonService : IPhotonPeerListener
{
    public static PhotonService Instance { get; private set; }

    private PhotonPeer peer;

    private event Action<bool> onConnectChange;
    public event Action<bool> OnConnectChange
    {
        add { onConnectChange += value; }
        remove { onConnectChange -= value; }
    }

    private bool serverConnected;
    public bool ServerConnected
    {
        get { return serverConnected; }
        private set
        {
            serverConnected = value;
            if (onConnectChange != null)
            {
                onConnectChange(serverConnected);
            }
            else
            {
                DebugReturn(DebugLevel.ERROR, "onConnectChange event is null");
            }
        }
    }

    static PhotonService()
    {
        Instance = new PhotonService();
    }

    private PhotonService()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Tcp);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        LogService.InfoFormat("{0}:{1}", level, message);
    }

    public void OnEvent(EventData eventData)
    {
        string errorMessage;
        if(!EndPointManager.EndPoint.EventManager.Operate((EndPointEventCode)eventData.Code, eventData.Parameters, out errorMessage))
        {
            LogService.ErrorFormat("Event Fail, ErrorMessage: {0}", errorMessage);
        }
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        string errorMessage;
        if (!EndPointManager.EndPoint.ResponseManager.Operate((EndPointOperationCode)operationResponse.OperationCode, (ReturnCode)operationResponse.ReturnCode, operationResponse.DebugMessage, operationResponse.Parameters, out errorMessage))
        {
            LogService.ErrorFormat("OperationResponse Fail, ErrorMessage: {0}", errorMessage);
        }
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                peer.EstablishEncryption();
                break;
            case StatusCode.Disconnect:
                ServerConnected = false;
                break;
            case StatusCode.EncryptionEstablished:
                ServerConnected = true;
                break;
        }
    }

    public void Connect(string serverName, string serverAddress, int port)
    {
        if (peer.Connect(serverAddress + ":" + port.ToString(), serverName))
        {
            DebugReturn(DebugLevel.INFO, peer.PeerState.ToString());
        }
        else
        {
            DebugReturn(DebugLevel.ERROR, "Connect Fail");
            ServerConnected = false;
        }
    }

    public void Service()
    {
        peer.Service();
    }

    public void Disconnect()
    {
        peer.Disconnect();
    }

    public void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
    {
        peer.OpCustom((byte)operationCode, parameters, true, 0, true);
    }
}

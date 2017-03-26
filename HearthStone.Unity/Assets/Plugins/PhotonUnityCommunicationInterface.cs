using HearthStone.Library;
using HearthStone.Library.CommunicationInfrastructure;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

public class PhotonUnityCommunicationInterface : CommunicationInterface
{
    public override void SendEvent(EndPointEventCode eventCode, Dictionary<byte, object> parameters)
    {
        LogService.FatalFormat("EndPoint SendEvent UserEventCode: {0}", eventCode);
    }

    public override void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
    {
        PhotonService.Instance.SendOperation(operationCode, parameters);
    }

    public override void SendResponse(EndPointOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
    {
        LogService.FatalFormat("EndPoint SendResponse UserOperationCode: {0}", operationCode);
    }
}

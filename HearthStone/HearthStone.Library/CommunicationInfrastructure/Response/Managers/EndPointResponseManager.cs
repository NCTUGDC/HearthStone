using HearthStone.Library.CommunicationInfrastructure.Response.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Response.Handlers.EndPointResponseHandlers;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.ResponseParameters.EndPointResponseParameterCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Managers
{
    public class EndPointResponseManager
    {
        private readonly EndPoint endPoint;
        protected readonly Dictionary<EndPointOperationCode, ResponseHandler<EndPoint, EndPointOperationCode>> operationTable = new Dictionary<EndPointOperationCode, ResponseHandler<EndPoint, EndPointOperationCode>>();

        public EndPointResponseManager(EndPoint endPoint)
        {
            this.endPoint = endPoint;
            operationTable.Add(EndPointOperationCode.PlayerOperation, new PlayerOperationResponseBroker(endPoint));
        }
        public void Operate(EndPointOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LogService.ErrorFormat($"EndPointResponse Error: {operationCode} from EndPoint: {endPoint.LastConnectedIPAddress}");
                }
            }
            else
            {
                LogService.ErrorFormat($"Unknow EndPointResponse:{operationCode} from EndPoint: {endPoint.LastConnectedIPAddress}");
            }
        }
        public void SendResponse(EndPointOperationCode operationCode, ReturnCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            endPoint.CommunicationInterface.SendResponse(operationCode, errorCode, debugMessage, parameters);
        }

        public void SendPlayerResponse(Player player, PlayerOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseData = new Dictionary<byte, object>
            {
                { (byte)PlayerResponseParameterCode.PlayerID, player.PlayerID },
                { (byte)PlayerResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)PlayerResponseParameterCode.ReturnCode, (short)returnCode },
                { (byte)PlayerResponseParameterCode.DebugMessage, debugMessage },
                { (byte)PlayerResponseParameterCode.Parameters, parameters }
            };
            SendResponse(EndPointOperationCode.PlayerOperation, ReturnCode.Correct, null, responseData);
        }
    }
}

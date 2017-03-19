using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.EndPointOperationHandlers;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;
using HearthStone.Protocol.Communication.OperationParameters.EndPointParameterCodes;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Managers
{
    public class EndPointOperationManager
    {
        private readonly EndPoint endPoint;
        private readonly Dictionary<EndPointOperationCode, OperationHandler<EndPoint, EndPointOperationCode>> operationTable = new Dictionary<EndPointOperationCode, OperationHandler<EndPoint, EndPointOperationCode>>();

        public EndPointOperationManager(EndPoint endPoint)
        {
            this.endPoint = endPoint;

            operationTable.Add(EndPointOperationCode.PlayerOperation, new PlayerOperationBroker(endPoint));
        }
        public void Operate(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if(operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LogService.ErrorFormat($"EndPointOperation Error: {operationCode} from EndPoint: {endPoint.LastConnectedIPAddress}");
                }
            }
            else
            {
                LogService.ErrorFormat($"Unknow EndPointOperation:{operationCode} from EndPoint: {endPoint.LastConnectedIPAddress}");
            }
        }
        public void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            endPoint.CommunicationInterface.SendOperation(operationCode, parameters);
        }

        public void SendPlayerOperation(Player player, PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> operationParameters = new Dictionary<byte, object>
            {
                { (byte)PlayerOperationParameterCode.PlayerID, player.PlayerID },
                { (byte)PlayerOperationParameterCode.OperationCode, operationCode },
                { (byte)PlayerOperationParameterCode.Parameters, parameters }
            };
            SendOperation(EndPointOperationCode.PlayerOperation, operationParameters);
        }
    }
}

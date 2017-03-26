using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.EndPointOperationHandlers;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataParameters;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.EndPointParameterCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Managers
{
    public class EndPointOperationManager
    {
        private readonly EndPoint endPoint;
        private readonly Dictionary<EndPointOperationCode, OperationHandler<EndPoint, EndPointOperationCode>> operationTable = new Dictionary<EndPointOperationCode, OperationHandler<EndPoint, EndPointOperationCode>>();
        public EndPointFetchDataBroker FetchDataBroker { get; private set; }

        internal EndPointOperationManager(EndPoint endPoint)
        {
            this.endPoint = endPoint;
            FetchDataBroker = new EndPointFetchDataBroker(endPoint);

            operationTable.Add(EndPointOperationCode.FetchData, FetchDataBroker);
            operationTable.Add(EndPointOperationCode.PlayerOperation, new PlayerOperationBroker(endPoint));
        }
        public bool Operate(EndPointOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(operationTable.ContainsKey(operationCode))
            {
                if (operationTable[operationCode].Handle(operationCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"EndPointOperation Error: {operationCode} from EndPoint: {endPoint.LastConnectedIPAddress}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow EndPointOperation:{operationCode} from EndPoint: {endPoint.LastConnectedIPAddress}";
                return false;
            }
        }
        internal void SendOperation(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            endPoint.CommunicationInterface.SendOperation(operationCode, parameters);
        }

        internal void SendFetchDataOperation(EndPointFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            SendOperation(EndPointOperationCode.FetchData, fetchDataParameters);
        }
        internal void SendPlayerOperation(Player player, PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
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

using HearthStone.Library.CommunicationInfrastructure.Response.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Response.Handlers.EndPointResponseHandlers;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.ResponseParameters.EndPoint;
using System;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Managers
{
    public class EndPointResponseManager
    {
        private readonly EndPoint endPoint;
        protected readonly Dictionary<EndPointOperationCode, ResponseHandler<EndPoint, EndPointOperationCode>> operationTable = new Dictionary<EndPointOperationCode, ResponseHandler<EndPoint, EndPointOperationCode>>();

        private event Action<ReturnCode, string> onRegisterResponse;
        public event Action<ReturnCode, string> OnRegisterResponse { add { onRegisterResponse += value; } remove { onRegisterResponse -= value; } }

        private event Action<ReturnCode, string> onLoginResponse;
        public event Action<ReturnCode, string> OnLoginResponse { add { onLoginResponse += value; } remove { onLoginResponse -= value; } }

        internal EndPointResponseManager(EndPoint endPoint)
        {
            this.endPoint = endPoint;

            operationTable.Add(EndPointOperationCode.FetchData, new FetchDataResponseBroker(endPoint));
            operationTable.Add(EndPointOperationCode.PlayerOperation, new PlayerOperationResponseBroker(endPoint));
            operationTable.Add(EndPointOperationCode.Register, new RegisterResponseHandler(endPoint));
            operationTable.Add(EndPointOperationCode.Login, new LoginResponseHandler(endPoint));
        }
        public bool Operate(EndPointOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"EndPointResponse Error: {operationCode} from EndPoint: {endPoint.LastConnectedIPAddress}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow EndPointResponse:{operationCode} from EndPoint: {endPoint.LastConnectedIPAddress}";
                return false;
            }
        }
        internal void SendResponse(EndPointOperationCode operationCode, ReturnCode errorCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            endPoint.CommunicationInterface.SendResponse(operationCode, errorCode, operationMessage, parameters);
        }
        internal void SendPlayerResponse(Player player, PlayerOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> responseData = new Dictionary<byte, object>
            {
                { (byte)PlayerResponseParameterCode.PlayerID, player.PlayerID },
                { (byte)PlayerResponseParameterCode.OperationCode, (byte)operationCode },
                { (byte)PlayerResponseParameterCode.ReturnCode, (short)returnCode },
                { (byte)PlayerResponseParameterCode.OperationMessage, operationMessage },
                { (byte)PlayerResponseParameterCode.Parameters, parameters }
            };
            SendResponse(EndPointOperationCode.PlayerOperation, ReturnCode.Correct, null, responseData);
        }

        internal void RegisterResponse(ReturnCode returnCode, string operationMessage)
        {
            onRegisterResponse?.Invoke(returnCode, operationMessage);
        }
        internal void LoginResponse(ReturnCode returnCode, string operationMessage)
        {
            onLoginResponse?.Invoke(returnCode, operationMessage);
        }
    }
}

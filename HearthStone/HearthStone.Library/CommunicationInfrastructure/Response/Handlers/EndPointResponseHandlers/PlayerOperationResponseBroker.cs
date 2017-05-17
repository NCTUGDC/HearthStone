using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.ResponseParameters.EndPoint;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.EndPointResponseHandlers
{
    internal class PlayerOperationResponseBroker : ResponseHandler<EndPoint, EndPointOperationCode>
    {
        internal PlayerOperationResponseBroker(EndPoint subject) : base(subject, 5)
        {
        }

        internal override bool Handle(EndPointOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                int playerID = (int)parameters[(byte)PlayerResponseParameterCode.PlayerID];
                PlayerOperationCode resolvedOperationCode = (PlayerOperationCode)parameters[(byte)PlayerResponseParameterCode.OperationCode];
                ReturnCode resolvedReturnCode = (ReturnCode)parameters[(byte)PlayerResponseParameterCode.ReturnCode];
                string resolvedOperationMessage = (string)parameters[(byte)PlayerResponseParameterCode.OperationMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerResponseParameterCode.Parameters];

                if (subject.Player.PlayerID == playerID)
                {
                    return subject.Player.ResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedOperationMessage, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"PlayerOperationResponse Error Player ID: {playerID} Not in EndPoint: {subject.LastConnectedIPAddress}";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

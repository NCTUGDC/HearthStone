using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.ResponseParameters.EndPointResponseParameterCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.EndPointResponseHandlers
{
    internal class PlayerOperationResponseBroker : ResponseHandler<EndPoint, EndPointOperationCode>
    {
        public PlayerOperationResponseBroker(EndPoint subject) : base(subject)
        {
        }

        internal override bool Handle(EndPointOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters))
            {
                int playerID = (int)parameters[(byte)PlayerResponseParameterCode.PlayerID];
                PlayerOperationCode resolvedOperationCode = (PlayerOperationCode)parameters[(byte)PlayerResponseParameterCode.OperationCode];
                ReturnCode resolvedReturnCode = (ReturnCode)parameters[(byte)PlayerResponseParameterCode.ReturnCode];
                string resolvedDebugMessage = (string)parameters[(byte)PlayerResponseParameterCode.DebugMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerResponseParameterCode.Parameters];

                if (subject.Player.PlayerID == playerID)
                {
                    subject.Player.ResponseManager.Operate(resolvedOperationCode, resolvedReturnCode, resolvedDebugMessage, resolvedParameters);
                    return true;
                }
                else
                {
                    LogService.ErrorFormat($"PlayerOperationResponse Error Player ID: {playerID} Not in EndPoint: {subject.LastConnectedIPAddress}");
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

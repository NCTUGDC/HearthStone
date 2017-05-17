using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.EndPoint;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.EndPointOperationHandlers
{
    internal class PlayerOperationBroker : EndPointOperationHandler
    {
        internal PlayerOperationBroker(EndPoint subject) : base(subject, 3)
        {

        }

        internal override bool Handle(EndPointOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                int playerID = (int)parameters[(byte)PlayerOperationParameterCode.PlayerID];
                PlayerOperationCode resolvedOperationCode = (PlayerOperationCode)parameters[(byte)PlayerOperationParameterCode.OperationCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerOperationParameterCode.Parameters];
                if (subject.Player.PlayerID == playerID)
                {
                    return subject.Player.OperationManager.Operate(resolvedOperationCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"PlayerOperation Error PlayerID: {playerID} Not in EndPoint: {subject.LastConnectedIPAddress}";
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

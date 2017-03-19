using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.EndPointParameterCodes;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.EndPointOperationHandlers
{
    internal class PlayerOperationBroker : EndPointOperationHandler
    {
        public PlayerOperationBroker(EndPoint subject) : base(subject, 3)
        {

        }

        internal override bool Handle(EndPointOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (base.Handle(operationCode, parameters))
            {
                int playerID = (int)parameters[(byte)PlayerOperationParameterCode.PlayerID];
                PlayerOperationCode resolvedOperationCode = (PlayerOperationCode)parameters[(byte)PlayerOperationParameterCode.OperationCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerOperationParameterCode.Parameters];
                if (subject.Player.PlayerID == playerID)
                {
                    subject.Player.OperationManager.Operate(resolvedOperationCode, resolvedParameters);
                    return true;
                }
                else
                {
                    LogService.ErrorFormat($"PlayerOperation Error PlayerID: {playerID} Not in EndPoint: {subject.LastConnectedIPAddress}");
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

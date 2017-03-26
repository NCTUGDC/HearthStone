using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers
{
    public class PlayerOperationHandler : OperationHandler<Player, PlayerOperationCode>
    {
        internal PlayerOperationHandler(Player subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        internal override void SendResponse(PlayerOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            subject.ResponseManager.SendResponse(operationCode, returnCode, operationMessage, parameters);
        }
    }
}

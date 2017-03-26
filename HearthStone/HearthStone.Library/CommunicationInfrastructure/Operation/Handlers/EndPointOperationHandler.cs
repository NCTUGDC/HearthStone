using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers
{
    public class EndPointOperationHandler : OperationHandler<EndPoint, EndPointOperationCode>
    {
        internal EndPointOperationHandler(EndPoint subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        internal override void SendResponse(EndPointOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameter)
        {
            subject.ResponseManager.SendResponse(operationCode, returnCode, operationMessage, parameter);
        }
    }
}

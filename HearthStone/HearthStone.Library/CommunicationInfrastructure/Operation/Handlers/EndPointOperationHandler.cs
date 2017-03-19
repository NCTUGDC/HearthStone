using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers
{
    public class EndPointOperationHandler : OperationHandler<EndPoint, EndPointOperationCode>
    {
        public EndPointOperationHandler(EndPoint subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        internal override void SendError(EndPointOperationCode operationCode, ReturnCode returnCode, string debugMessage)
        {
            base.SendError(operationCode, returnCode, debugMessage);
            Dictionary<byte, object> parameters = new Dictionary<byte, object>();
            subject.ResponseManager.SendResponse(operationCode, returnCode, debugMessage, parameters);
        }
        internal override void SendResponse(EndPointOperationCode operationCode, Dictionary<byte, object> parameter)
        {
            subject.ResponseManager.SendResponse(operationCode, ReturnCode.Correct, null, parameter);
        }
    }
}

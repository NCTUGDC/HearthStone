using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.EndPointResponseHandlers
{
    class RegisterResponseHandler : ResponseHandler<EndPoint, EndPointOperationCode>
    {
        public RegisterResponseHandler(EndPoint subject) : base(subject, 0)
        {
        }

        internal override bool Handle(EndPointOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            return base.Handle(operationCode, returnCode, operationMessage, parameters, out errorMessage);
        }
        internal override bool CheckError(EndPointOperationCode operationCode, Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage)
        {
            if(base.CheckError(operationCode, parameters, returnCode, operationMessage, out errorMessage))
            {
                subject.ResponseManager.RegisterResponse(returnCode, operationMessage);
                return true;
            }
            else
            {
                subject.ResponseManager.RegisterResponse(returnCode, operationMessage);
                return false;
            }
        }
    }
}

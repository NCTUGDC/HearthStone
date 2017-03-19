using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers
{
    public abstract class ResponseHandler<TSubject, TOperationCode>
    {
        protected TSubject subject;
        protected int correctParameterCount;

        protected ResponseHandler(TSubject subject)
        {
            this.subject = subject;
        }

        internal virtual bool Handle(TOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (CheckError(operationCode, parameters, returnCode, debugMessage))
            {
                return true;
            }
            else
            {
                LogService.ErrorFormat($"Error On {subject.GetType()}  OperationCode: {operationCode}, ReturnCode: {returnCode}, DebugMessage: {debugMessage}");
                return false;
            }
        }
        internal virtual bool CheckError(TOperationCode operationCode, Dictionary<byte, object> parameters, ReturnCode returnCode, string debugMessage)
        {
            switch (returnCode)
            {
                case ReturnCode.Correct:
                    return parameters.Count == correctParameterCount;
                default:
                    LogService.ErrorFormat($"OperationResponse: {operationCode} Unknown ReturnCode: {returnCode}, DebugMessage: {debugMessage}");
                    return false;
            }
        }
    }
}

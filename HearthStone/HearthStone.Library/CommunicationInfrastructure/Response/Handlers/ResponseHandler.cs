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

        internal virtual bool Handle(TOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (CheckError(operationCode, parameters, returnCode, operationMessage, out errorMessage))
            {
                return true;
            }
            else
            {
                errorMessage = $"Error On {subject.GetType()}  OperationCode: {operationCode}, ReturnCode: {returnCode}, OperationMessage: {operationMessage}, ErrorMessage: {errorMessage}";
                return false;
            }
        }
        internal virtual bool CheckError(TOperationCode operationCode, Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage)
        {
            switch (returnCode)
            {
                case ReturnCode.Correct:
                    return CheckParameters(parameters, out errorMessage);
                default:
                    errorMessage = $"OperationResponse: {operationCode} Unknown ReturnCode: {returnCode}, OperationMessage: {operationMessage}";
                    return false;
            }
        }
        internal virtual bool CheckParameters(Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                errorMessage = string.Format("Parameter Count: {0} Should be {1}", parameters.Count, correctParameterCount);
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }
    }
}

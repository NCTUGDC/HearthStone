using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers
{
    public abstract class OperationHandler<TSubject, TOperationCode>
    {
        protected TSubject subject;
        protected int correctParameterCount;

        protected OperationHandler(TSubject subject, int correctParameterCount)
        {
            this.subject = subject;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(TOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            ReturnCode errorCode;
            if (CheckParameters(parameters, out errorCode, out errorMessage))
            {
                return true;
            }
            else
            {
                SendResponse(operationCode, errorCode, errorMessage, new Dictionary<byte, object>());
                return false;
            }
        }
        internal virtual bool CheckParameters(Dictionary<byte, object> parameters, out ReturnCode errorCode, out string errorMessage)
        {
            if (parameters.Count == correctParameterCount)
            {
                errorCode = ReturnCode.Correct;
                errorMessage = "";
                return true;
            }
            else
            {
                errorCode = ReturnCode.ParameterCountError;
                errorMessage = $"Parameter Count: {parameters.Count} Should be {correctParameterCount}";
                return false;
            }
        }
        internal abstract void SendResponse(TOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters);
    }
}

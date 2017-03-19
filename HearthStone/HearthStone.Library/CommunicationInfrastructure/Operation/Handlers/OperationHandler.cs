using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers
{
    public abstract class OperationHandler<TSubject, TOperationCode>
    {
        protected TSubject subject;
        protected int correctParameterCount;

        internal OperationHandler(TSubject subject, int correctParameterCount)
        {
            this.subject = subject;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(TOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameterCount(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                SendError(operationCode, ReturnCode.ParameterCountError, debugMessage);
                return false;
            }
        }
        internal virtual bool CheckParameterCount(Dictionary<byte, object> parameters, out string debugMessage)
        {
            if (parameters.Count == correctParameterCount)
            {
                debugMessage = "";
                return true;
            }
            else
            {
                debugMessage = string.Format($"Parameter Count: {parameters.Count} Should be {correctParameterCount}");
                return false;
            }
        }
        internal virtual void SendError(TOperationCode operationCode, ReturnCode returnCode, string debugMessage)
        {
            LogService.ErrorFormat($"Error On {subject.GetType()}  OperationCode: {operationCode}, ReturnCode: {returnCode}, DebugMessage: {debugMessage}");
        }
        internal abstract void SendResponse(TOperationCode operationCode, Dictionary<byte, object> parameter);
    }
}

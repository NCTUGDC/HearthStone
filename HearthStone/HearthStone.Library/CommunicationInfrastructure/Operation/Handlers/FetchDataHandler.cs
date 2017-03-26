using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers
{
    public abstract class FetchDataHandler<TSubject, TFetchDataCode>
    {
        protected TSubject subject;
        protected int correctParameterCount;

        protected FetchDataHandler(TSubject subject, int correctParameterCount)
        {
            this.subject = subject;
            this.correctParameterCount = correctParameterCount;
        }

        public virtual bool Handle(TFetchDataCode fetchCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            ReturnCode errorCode; ;
            if (CheckParameters(parameters, out errorCode, out errorMessage))
            {
                return true;
            }
            else
            {
                SendResponse(fetchCode, errorCode, errorMessage, new Dictionary<byte, object>());
                return false;
            }
        }
        internal virtual bool CheckParameters(Dictionary<byte, object> parameters, out ReturnCode errorCode, out string errorMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                errorCode = ReturnCode.ParameterCountError;
                errorMessage = $"Parameter Count: {parameters.Count} Should be {correctParameterCount}";
                return false;
            }
            else
            {
                errorCode = ReturnCode.Correct;
                errorMessage = "";
                return true;
            }
        }
        public abstract void SendResponse(TFetchDataCode fetchCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters);
    }
}

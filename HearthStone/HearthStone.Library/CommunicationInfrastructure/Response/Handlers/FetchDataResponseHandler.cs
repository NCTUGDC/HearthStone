using HearthStone.Protocol;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers
{
    public abstract class FetchDataResponseHandler<TSubject, TFetchDataCode>
    {
        protected TSubject subject;

        protected FetchDataResponseHandler(TSubject subject)
        {
            this.subject = subject;
        }

        public virtual bool Handle(TFetchDataCode fetchCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (CheckError(parameters, returnCode, operationMessage, out errorMessage))
            {
                return true;
            }
            else
            {
                errorMessage = $"{subject.GetType()} FetchData Parameter Error On {fetchCode}, Identity: {subject.ToString()}, ErrorMessage: {errorMessage}";
                return false;
            }
        }
        public abstract bool CheckError(Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage);
    }
}

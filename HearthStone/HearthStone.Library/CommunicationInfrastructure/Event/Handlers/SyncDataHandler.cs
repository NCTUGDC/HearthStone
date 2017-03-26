using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers
{
    internal abstract class SyncDataHandler<TSubject, TSyncDataCode>
    {
        protected TSubject subject;
        protected int correctParameterCount;

        protected SyncDataHandler(TSubject subject, int correctParameterCount)
        {
            this.subject = subject;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(TSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (CheckParameters(parameters, out errorMessage))
            {
                return true;
            }
            else
            {
                errorMessage = $"{subject.GetType()} SyncData Parameter Error On {syncCode} Identity: {subject.ToString()}, Debug Message: {errorMessage}";
                return false;
            }
        }
        internal virtual bool CheckParameters(Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (parameters.Count != correctParameterCount)
            {
                errorMessage = $"Parameter Count: {parameters.Count} Should be {correctParameterCount}";
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

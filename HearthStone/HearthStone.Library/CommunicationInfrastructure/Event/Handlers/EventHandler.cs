using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers
{
    public abstract class EventHandler<TSubject, TEventCode>
    {
        protected TSubject subject;
        protected int correctParameterCount;

        protected EventHandler(TSubject subject, int correctParameterCount)
        {
            this.subject = subject;
            this.correctParameterCount = correctParameterCount;
        }

        internal virtual bool Handle(TEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (CheckParameters(parameters, out errorMessage))
            {
                return true;
            }
            else
            {
                errorMessage = $"Error On {subject.GetType()}  EventCode: {eventCode}, DebugMessage: {errorMessage}";
                return false;
            }
        }
        internal virtual bool CheckParameters(Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (parameters.Count == correctParameterCount)
            {
                errorMessage = "";
                return true;
            }
            else
            {
                errorMessage = string.Format($"Parameter Count: {parameters.Count} Should be {correctParameterCount}");
                return false;
            }
        }
    }
}

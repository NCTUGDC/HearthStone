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

        internal virtual bool Handle(TEventCode eventCode, Dictionary<byte, object> parameters)
        {
            string debugMessage;
            if (CheckParameterCount(parameters, out debugMessage))
            {
                return true;
            }
            else
            {
                LogService.ErrorFormat($"Error On {subject.GetType()}  EventCode: {eventCode}, DebugMessage: {debugMessage}");
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
    }
}

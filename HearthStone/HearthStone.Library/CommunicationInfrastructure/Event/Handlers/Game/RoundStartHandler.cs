using HearthStone.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game
{
    class RoundStartHandler : EventHandler<Library.Game, GameEventCode>
    {
        public RoundStartHandler(Library.Game subject) : base(subject, 0)
        {
        }

        internal override bool Handle(GameEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                subject.EventManager.RoundStartEvent();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

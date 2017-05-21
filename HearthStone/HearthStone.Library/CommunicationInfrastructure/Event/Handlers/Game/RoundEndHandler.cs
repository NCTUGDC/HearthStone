using HearthStone.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game
{
    class RoundEndHandler : EventHandler<Library.Game, GameEventCode>
    {
        public RoundEndHandler(Library.Game subject) : base(subject, 0)
        {
        }

        internal override bool Handle(GameEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                subject.EventManager.RoundEndEvent();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

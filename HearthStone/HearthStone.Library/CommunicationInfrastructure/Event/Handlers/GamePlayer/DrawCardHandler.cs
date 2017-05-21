using HearthStone.Protocol.Communication.EventCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.GamePlayer
{
    class DrawCardHandler : EventHandler<Library.GamePlayer, GamePlayerEventCode>
    {
        public DrawCardHandler(Library.GamePlayer subject) : base(subject, 0)
        {
        }

        internal override bool Handle(GamePlayerEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                subject.EventManager.DrawCardEvent();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

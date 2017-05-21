using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.Game;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game
{
    class GameOverHandler : EventHandler<Library.Game, GameEventCode>
    {
        public GameOverHandler(Library.Game subject) : base(subject, 1)
        {
        }

        internal override bool Handle(GameEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                int winnerGamePlayerID = (int)parameters[(byte)GameOverParameterCode.WinnerGamePlayerID];
                subject.EventManager.GameOverEvent(winnerGamePlayerID);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

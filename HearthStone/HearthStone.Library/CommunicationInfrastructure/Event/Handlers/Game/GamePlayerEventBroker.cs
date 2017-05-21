using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.Game;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game
{
    public class GamePlayerEventBroker : EventHandler<Library.Game, GameEventCode>
    {
        public GamePlayerEventBroker(Library.Game subject) : base(subject, 3)
        {
        }

        internal override bool Handle(GameEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                int gamePlayerID = (int)parameters[(byte)GamePlayerEventParameterCode.GamePlayerID];
                GamePlayerEventCode resolvedEventCode = (GamePlayerEventCode)parameters[(byte)GamePlayerEventParameterCode.EventCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)GamePlayerEventParameterCode.Parameters];

                if (gamePlayerID == 1)
                {
                    return subject.GamePlayer1.EventManager.Operate(resolvedEventCode, resolvedParameters, out errorMessage);
                }
                else if (gamePlayerID == 2)
                {
                    return subject.GamePlayer2.EventManager.Operate(resolvedEventCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"GamePlayerEvent Error GamePlayer ID: {gamePlayerID} Not in Game: {subject.GameID}";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.EndPoint;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPoint
{
    public class GameEventBroker : EventHandler<Library.EndPoint, EndPointEventCode>
    {
        internal GameEventBroker(Library.EndPoint subject) : base(subject, 3)
        {
        }
        internal override bool Handle(EndPointEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                int gameID = (int)parameters[(byte)GameEventParameterCode.GameID];
                GameEventCode resolvedEventCode = (GameEventCode)parameters[(byte)GameEventParameterCode.EventCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)GameEventParameterCode.Parameters];
                Library.Game game;
                if (GameManager.Instance.FindGame(gameID, out game))
                {
                    return game.EventManager.Operate(resolvedEventCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"GameEvent Error Game ID: {gameID} Not in GameManager: {subject.LastConnectedIPAddress}";
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

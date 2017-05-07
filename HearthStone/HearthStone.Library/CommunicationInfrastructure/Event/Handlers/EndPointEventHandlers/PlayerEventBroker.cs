using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.EndPoint;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPointEventHandlers
{
    public class PlayerEventBroker : EventHandler<EndPoint, EndPointEventCode>
    {
        internal PlayerEventBroker(EndPoint subject) : base(subject, 3)
        {
        }
        internal override bool Handle(EndPointEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(eventCode, parameters, out errorMessage))
            {
                int playerID = (int)parameters[(byte)PlayerEventParameterCode.PlayerID];
                PlayerEventCode resolvedEventCode = (PlayerEventCode)parameters[(byte)PlayerEventParameterCode.EventCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerEventParameterCode.Parameters];

                if (subject.Player.PlayerID == playerID)
                {
                    return subject.Player.EventManager.Operate(resolvedEventCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"PlayerEvent Error Player ID: {playerID} Not in EndPoint: {subject.LastConnectedIPAddress}";
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

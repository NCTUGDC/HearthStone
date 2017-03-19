using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.EndPointEventParameterCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPointEventHandlers
{
    internal class PlayerEventBroker : EventHandler<EndPoint, EndPointEventCode>
    {
        public PlayerEventBroker(EndPoint subject) : base(subject, 3)
        {
        }
        internal override bool Handle(EndPointEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if(base.Handle(eventCode, parameters))
            {
                int playerID = (int)parameters[(byte)PlayerEventParameterCode.PlayerID];
                PlayerEventCode resolvedEventCode = (PlayerEventCode)parameters[(byte)PlayerEventParameterCode.EventCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)PlayerEventParameterCode.Parameters];

                if (subject.Player.PlayerID == playerID)
                {
                    subject.Player.EventManager.Operate(resolvedEventCode, resolvedParameters);
                    return true;
                }
                else
                {
                    LogService.ErrorFormat($"PlayerEvent Error Player ID: {playerID} Not in EndPoint: {subject.LastConnectedIPAddress}");
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

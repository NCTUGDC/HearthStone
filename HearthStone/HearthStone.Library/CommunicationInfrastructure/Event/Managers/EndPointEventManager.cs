using HearthStone.Library.CommunicationInfrastructure.Event.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPointEventHandlers;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.EndPointEventParameterCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Managers
{
    public class EndPointEventManager
    {
        private readonly EndPoint endPoint;
        private readonly Dictionary<EndPointEventCode, EventHandler<EndPoint, EndPointEventCode>> eventTable = new Dictionary<EndPointEventCode, EventHandler<EndPoint, EndPointEventCode>>();

        public EndPointEventManager(EndPoint endPoint)
        {
            this.endPoint = endPoint;
            eventTable.Add(EndPointEventCode.PlayerEvent, new PlayerEventBroker(endPoint));
        }
        public void Operate(EndPointEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LogService.ErrorFormat($"EndPointEvent Error: {eventCode} from EndPoint: {endPoint.LastConnectedIPAddress}");
                }
            }
            else
            {
                LogService.ErrorFormat($"Unknow EndPointEvent:{eventCode} from EndPoint: {endPoint.LastConnectedIPAddress}");
            }
        }
        internal void SendEvent(EndPointEventCode eventCode, Dictionary<byte, object> parameters)
        {
            endPoint.CommunicationInterface.SendEvent(eventCode, parameters);
        }
        internal void SendPlayerEvent(Player player, PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)PlayerEventParameterCode.PlayerID, player.PlayerID },
                { (byte)PlayerEventParameterCode.EventCode, (byte)eventCode },
                { (byte)PlayerEventParameterCode.Parameters, parameters }
            };
            SendEvent(EndPointEventCode.PlayerEvent, eventData);
        }
    }
}

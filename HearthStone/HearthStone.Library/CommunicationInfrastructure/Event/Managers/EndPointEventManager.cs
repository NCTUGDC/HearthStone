using HearthStone.Library.CommunicationInfrastructure.Event.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPointEventHandlers;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.EndPointEventParameterCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Managers
{
    public class EndPointEventManager
    {
        private readonly EndPoint endPoint;
        private readonly Dictionary<EndPointEventCode, EventHandler<EndPoint, EndPointEventCode>> eventTable = new Dictionary<EndPointEventCode, EventHandler<EndPoint, EndPointEventCode>>();
        public EndPointSyncDataBroker SyncDataBroker { get; private set; }

        internal EndPointEventManager(EndPoint endPoint)
        {
            this.endPoint = endPoint;
            SyncDataBroker = new EndPointSyncDataBroker(endPoint);

            eventTable.Add(EndPointEventCode.SyncData, SyncDataBroker);
            eventTable.Add(EndPointEventCode.PlayerEvent, new PlayerEventBroker(endPoint));
        }
        public bool Operate(EndPointEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (eventTable[eventCode].Handle(eventCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"EndPointEvent Error: {eventCode} from EndPoint: {endPoint.LastConnectedIPAddress}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow EndPointEvent:{eventCode} from EndPoint: {endPoint.LastConnectedIPAddress}";
                return false;
            }
        }
        internal void SendEvent(EndPointEventCode eventCode, Dictionary<byte, object> parameters)
        {
            endPoint.CommunicationInterface.SendEvent(eventCode, parameters);
        }
        internal void SendSyncDataEvent(EndPointSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> syncDataParameters = new Dictionary<byte, object>
            {
                { (byte)SyncDataEventParameterCode.SyncDataCode, (byte)syncCode },
                { (byte)SyncDataEventParameterCode.Parameters, parameters }
            };
            SendEvent(EndPointEventCode.SyncData, syncDataParameters);
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

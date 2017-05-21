using HearthStone.Library.CommunicationInfrastructure.Event.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Field;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Managers
{
    public class FieldEventManager
    {
        private readonly Field field;
        private readonly Dictionary<FieldEventCode, EventHandler<Field, FieldEventCode>> eventTable = new Dictionary<FieldEventCode, EventHandler<Field, FieldEventCode>>();
        public FieldSyncDataBroker SyncDataBroker { get; private set; }

        internal FieldEventManager(Field field)
        {
            this.field = field;
            SyncDataBroker = new FieldSyncDataBroker(field);

            eventTable.Add(FieldEventCode.SyncData, SyncDataBroker);
        }

        internal bool Operate(FieldEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (eventTable[eventCode].Handle(eventCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"Field Event Error: {eventCode} from FieldID: {field.FieldID}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow Field Event:{eventCode} from FieldID: {field.FieldID}";
                return false;
            }
        }

        internal void SendEvent(FieldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            field.Game.EventManager.SendFieldEvent(field, eventCode, parameters);
        }
        internal void SendSyncDataEvent(FieldSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> syncDataParameters = new Dictionary<byte, object>
            {
                { (byte)SyncDataEventParameterCode.SyncDataCode, (byte)syncCode },
                { (byte)SyncDataEventParameterCode.Parameters, parameters }
            };
            SendEvent(FieldEventCode.SyncData, syncDataParameters);
        }
    }
}

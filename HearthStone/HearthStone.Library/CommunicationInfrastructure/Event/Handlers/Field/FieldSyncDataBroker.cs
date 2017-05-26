using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Field.Sync;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.Field;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Field
{
    public class FieldSyncDataBroker : SyncDataResolver<Library.Field, FieldEventCode, FieldSyncDataCode>
    {
        internal FieldSyncDataBroker(Library.Field subject) : base(subject)
        {
            syncTable.Add(FieldSyncDataCode.FieldCardChanged, new SyncFieldCardChangedHandler(subject));
        }

        internal override void SendSyncData(FieldSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }

        public void SyncCardChanged(Library.Field.FieldCardRecord record, DataChangeCode changeCode)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncCardChangedParameterCode.DataChangeCode, (byte)changeCode },
                { (byte)SyncCardChangedParameterCode.FieldCardRecordByteArray, SerializationHelper.Serialize(record) },
            };
            SendSyncData(FieldSyncDataCode.FieldCardChanged, eventData);
        }
    }
}

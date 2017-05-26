using HearthStone.Protocol;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.Field;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Field.Sync
{
    class SyncFieldCardChangedHandler : SyncDataHandler<Library.Field, FieldSyncDataCode>
    {
        public SyncFieldCardChangedHandler(Library.Field subject) : base(subject, 2)
        {
        }
        internal override bool Handle(FieldSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                DataChangeCode changeCode = (DataChangeCode)parameters[(byte)SyncCardChangedParameterCode.DataChangeCode];
                Library.Field.FieldCardRecord record = SerializationHelper.Deserialize<Library.Field.FieldCardRecord>((byte[])parameters[(byte)SyncCardChangedParameterCode.FieldCardRecordByteArray]);
                switch(changeCode)
                {
                    case DataChangeCode.Add:
                        subject.AddCard(record.CardRecordID, record.PositionIndex);
                        break;
                    case DataChangeCode.Remove:
                        subject.RemoveCard(record.CardRecordID);
                        break;
                    case DataChangeCode.Update:
                        subject.UpdateCard(record.CardRecordID, record.PositionIndex);
                        break;
                    default:
                        errorMessage = "Undefined changeCode";
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

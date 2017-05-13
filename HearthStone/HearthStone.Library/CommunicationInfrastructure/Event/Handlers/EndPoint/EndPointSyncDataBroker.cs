using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPoint.Sync;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.EndPoint;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPoint
{
    public class EndPointSyncDataBroker : SyncDataResolver<Library.EndPoint, EndPointEventCode, EndPointSyncDataCode>
    {
        internal EndPointSyncDataBroker(Library.EndPoint subject) : base(subject)
        {
            syncTable.Add(EndPointSyncDataCode.WaitingPlayerCount, new SyncWaitingPlayerCountHandler(subject));
        }

        internal override void SendSyncData(EndPointSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }

        public void SyncWaitingPlayerCount(int waitingPlayerCount)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncWaitingPlayerCountParameterCode.PlayerCount, waitingPlayerCount }
            };
            SendSyncData(EndPointSyncDataCode.WaitingPlayerCount, eventData);
        }
    }
}

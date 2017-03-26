using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPointEventHandlers
{
    public class EndPointSyncDataBroker : SyncDataResolver<EndPoint, EndPointEventCode, EndPointSyncDataCode>
    {
        internal EndPointSyncDataBroker(EndPoint subject) : base(subject)
        {
        }

        internal override void SendSyncData(EndPointSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }
    }
}

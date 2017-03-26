using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.PlayerEventHandlers
{
    public class PlayerSyncDataBroker : SyncDataResolver<Player, PlayerEventCode, PlayerSyncDataCode>
    {
        internal PlayerSyncDataBroker(Player subject) : base(subject)
        {
        }

        internal override void SendSyncData(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }
    }
}

using HearthStone.Protocol;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game
{
    public class GameSyncDataBroker : SyncDataResolver<Library.Game, GameEventCode, GameSyncDataCode>
    {
        internal GameSyncDataBroker(Library.Game subject) : base(subject)
        {

        }

        internal override void SendSyncData(GameSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }
    }
}

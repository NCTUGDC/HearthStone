using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game.Sync;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.Game;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game
{
    public class GameSyncDataBroker : SyncDataResolver<Library.Game, GameEventCode, GameSyncDataCode>
    {
        internal GameSyncDataBroker(Library.Game subject) : base(subject)
        {
            syncTable.Add(GameSyncDataCode.RoundCountChanged, new SyncRoundCountChangedHandler(subject));
            syncTable.Add(GameSyncDataCode.CurrentGamePlayerID_Changed, new SyncCurrentGamePlayerID_ChangedHandler(subject));
        }

        internal override void SendSyncData(GameSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }

        public void SyncRoundCountChanged(Library.Game game)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncRoundCountChangedParameterCode.RoundCount, game.RoundCount }
            };
            SendSyncData(GameSyncDataCode.RoundCountChanged, eventData);
        }
        public void SyncCurrentGamePlayerID_Changed(Library.Game game)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncCurrentGamePlayerID_ChangedParameterCode.CurrentGamePlayerID, game.CurrentGamePlayerID }
            };
            SendSyncData(GameSyncDataCode.CurrentGamePlayerID_Changed, eventData);
        }
    }
}

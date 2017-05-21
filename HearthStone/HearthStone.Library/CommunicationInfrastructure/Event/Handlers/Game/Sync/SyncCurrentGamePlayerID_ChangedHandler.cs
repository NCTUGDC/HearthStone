using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.Game;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game.Sync
{
    class SyncCurrentGamePlayerID_ChangedHandler : SyncDataHandler<Library.Game, GameSyncDataCode>
    {
        public SyncCurrentGamePlayerID_ChangedHandler(Library.Game subject) : base(subject, 1)
        {
        }
        internal override bool Handle(GameSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                int currentGamePlayerID = (int)parameters[(byte)SyncCurrentGamePlayerID_ChangedParameterCode.CurrentGamePlayerID];
                subject.CurrentGamePlayerID = currentGamePlayerID;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

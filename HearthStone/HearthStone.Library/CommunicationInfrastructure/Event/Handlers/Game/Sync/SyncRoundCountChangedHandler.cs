using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.Game;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game.Sync
{
    class SyncRoundCountChangedHandler : SyncDataHandler<Library.Game, GameSyncDataCode>
    {
        public SyncRoundCountChangedHandler(Library.Game subject) : base(subject, 1)
        {
        }
        internal override bool Handle(GameSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                int roundCount = (int)parameters[(byte)SyncRoundCountChangedParameterCode.RoundCount];
                subject.RoundCount = roundCount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

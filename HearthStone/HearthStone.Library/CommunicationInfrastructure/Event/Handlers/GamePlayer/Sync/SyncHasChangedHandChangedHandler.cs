using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.GamePlayer;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.GamePlayer.Sync
{
    class SyncHasChangedHandChangedHandler : SyncDataHandler<Library.GamePlayer, GamePlayerSyncDataCode>
    {
        public SyncHasChangedHandChangedHandler(Library.GamePlayer subject) : base(subject, 1)
        {
        }
        internal override bool Handle(GamePlayerSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                bool hasChangedHand = (bool)parameters[(byte)SyncHasChangedHandChangedParameterCode.HasChangedHand];
                subject.HasChangedHand = hasChangedHand;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

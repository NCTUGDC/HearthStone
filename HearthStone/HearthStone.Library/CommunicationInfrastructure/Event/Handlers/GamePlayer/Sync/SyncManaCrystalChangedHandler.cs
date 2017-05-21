using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.GamePlayer;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.GamePlayer.Sync
{
    class SyncManaCrystalChangedHandler : SyncDataHandler<Library.GamePlayer, GamePlayerSyncDataCode>
    {
        public SyncManaCrystalChangedHandler(Library.GamePlayer subject) : base(subject, 1)
        {
        }
        internal override bool Handle(GamePlayerSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                int manaCrystal = (int)parameters[(byte)SyncManaCrystalChangedParameterCode.ManaCrystal];
                subject.ManaCrystal = manaCrystal;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using HearthStone.Protocol;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.GamePlayer;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.GamePlayer.Sync
{
    class SyncHandCardsChangedHandler : SyncDataHandler<Library.GamePlayer, GamePlayerSyncDataCode>
    {
        public SyncHandCardsChangedHandler(Library.GamePlayer subject) : base(subject, 2)
        {
        }
        internal override bool Handle(GamePlayerSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                DataChangeCode changeCode = (DataChangeCode)parameters[(byte)SyncHandCardsChangedParameterCode.DataChangeCode];
                int cardRecordID = (int)parameters[(byte)SyncHandCardsChangedParameterCode.CardRecordID];

                switch (changeCode)
                {
                    case DataChangeCode.Add:
                        subject.AddHandCard(cardRecordID);
                        break;
                    case DataChangeCode.Remove:
                        subject.RemoveHandCard(cardRecordID);
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

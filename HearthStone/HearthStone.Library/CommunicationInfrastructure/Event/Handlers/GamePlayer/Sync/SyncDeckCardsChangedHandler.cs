using HearthStone.Protocol;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.GamePlayer;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.GamePlayer.Sync
{
    class SyncDeckCardsChangedHandler : SyncDataHandler<Library.GamePlayer, GamePlayerSyncDataCode>
    {
        public SyncDeckCardsChangedHandler(Library.GamePlayer subject) : base(subject, 2)
        {
        }
        internal override bool Handle(GamePlayerSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                DataChangeCode changeCode = (DataChangeCode)parameters[(byte)SyncDeckCardsChangedParameterCode.DataChangeCode];
                int cardRecordID = (int)parameters[(byte)SyncDeckCardsChangedParameterCode.CardRecordID];

                switch (changeCode)
                {
                    case DataChangeCode.Add:
                        subject.Deck.AddCard(cardRecordID);
                        break;
                    case DataChangeCode.Remove:
                        subject.Deck.RemoveCard(cardRecordID);
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

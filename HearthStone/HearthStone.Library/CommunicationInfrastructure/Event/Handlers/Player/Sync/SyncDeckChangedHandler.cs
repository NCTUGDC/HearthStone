using HearthStone.Protocol;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Player.Sync
{
    class SyncDeckChangedHandler : SyncDataHandler<Library.Player, PlayerSyncDataCode>
    {
        public SyncDeckChangedHandler(Library.Player subject) : base(subject, 4)
        {
        }
        internal override bool Handle(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(syncCode, parameters, out errorMessage))
            {
                DataChangeCode changeCode = (DataChangeCode)parameters[(byte)SyncDeckChangedParameterCode.DataChangeCode];
                int deckID = (int)parameters[(byte)SyncDeckChangedParameterCode.DeckID];
                string deckName = (string)parameters[(byte)SyncDeckChangedParameterCode.DeckName];
                int maxCardCount = (int)parameters[(byte)SyncDeckChangedParameterCode.MaxCardCount];

                switch(changeCode)
                {
                    case DataChangeCode.Add:
                        subject.LoadDeck(new Deck(deckID, deckName, maxCardCount));
                        break;
                    case DataChangeCode.Remove:
                        subject.RemoveDeck(deckID);
                        break;
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

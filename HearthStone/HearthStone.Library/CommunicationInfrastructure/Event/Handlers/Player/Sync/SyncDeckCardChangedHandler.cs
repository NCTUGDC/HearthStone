using HearthStone.Protocol;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Player.Sync
{
    class SyncDeckCardChangedHandler : SyncDataHandler<Library.Player, PlayerSyncDataCode>
    {
        public SyncDeckCardChangedHandler(Library.Player subject) : base(subject, 3)
        {
        }
        internal override bool Handle(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                DataChangeCode changeCode = (DataChangeCode)parameters[(byte)SyncDeckCardChangedParameterCode.DataChangeCode];
                int deckID = (int)parameters[(byte)SyncDeckCardChangedParameterCode.DeckID];
                int cardID = (int)parameters[(byte)SyncDeckCardChangedParameterCode.CardID];
                Deck deck;
                Card card;
                if (subject.FindDeck(deckID, out deck) && CardManager.Instance.FindCard(cardID, out card))
                {
                    switch (changeCode)
                    {
                        case DataChangeCode.Add:
                            deck.AddCard(card);
                            break;
                        case DataChangeCode.Remove:
                            deck.RemoveCard(cardID);
                            break;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataParameters.Player;
using HearthStone.Protocol.Communication.FetchDataResponseParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers.Fetch
{
    class FetchAllDeckCardsHandler : PlayerFetchDataHandler
    {
        public FetchAllDeckCardsHandler(Player subject) : base(subject, 1)
        {
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(fetchCode, parameters, out errorMessage))
            {
                int deckID = (int)parameters[(byte)FetchAllDeckCardsParameterCode.DeckID];
                Deck deck;
                if(subject.FindDeck(deckID, out deck))
                {
                    foreach (var card in deck.Cards)
                    {
                        var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchAllDeckCardsResponseParameterCode.DeckID, deck.DeckID },
                            { (byte)FetchAllDeckCardsResponseParameterCode.CardID, card.CardID }
                        };
                        SendResponse(fetchCode, ReturnCode.Correct, "", result);
                    }
                    return true;
                }
                else
                {
                    errorMessage = "Deck Not Existed";
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

using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataResponseParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.PlayerResponseHandlers.Fetch
{
    class FetchAllDeckCardsResponseHandler : FetchDataResponseHandler<Player, PlayerFetchDataCode>
    {
        public FetchAllDeckCardsResponseHandler(Player subject) : base(subject)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage)
        {
            switch (returnCode)
            {
                case ReturnCode.Correct:
                    {
                        if (parameters.Count != 2)
                        {
                            errorMessage = $"Parameter Error, Parameter Count: {parameters.Count}";
                            return false;
                        }
                        else
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                default:
                    {
                        errorMessage = "Unknown Error";
                        return false;
                    }
            }
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(fetchCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                int deckID = (int)parameters[(byte)FetchAllDeckCardsResponseParameterCode.DeckID];
                int cardID = (int)parameters[(byte)FetchAllDeckCardsResponseParameterCode.CardID];
                Deck deck;
                if(subject.FindDeck(deckID, out deck))
                {
                    Card card;
                    if(CardManager.Instance.FindCard(cardID, out card))
                    {
                        deck.AddCard(card);
                        return true;
                    }
                    else
                    {
                        errorMessage = "Card Not Exist";
                        return false;
                    }
                }
                else
                {
                    errorMessage = "Deck Not Exist";
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

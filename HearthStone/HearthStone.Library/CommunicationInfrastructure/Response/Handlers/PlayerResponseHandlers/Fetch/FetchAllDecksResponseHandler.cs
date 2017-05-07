using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataResponseParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.PlayerResponseHandlers.Fetch
{
    class FetchAllDecksResponseHandler : FetchDataResponseHandler<Player, PlayerFetchDataCode>
    {
        public FetchAllDecksResponseHandler(Player subject) : base(subject)
        {
        }

        public override bool CheckError(Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage)
        {
            switch (returnCode)
            {
                case ReturnCode.Correct:
                    {
                        if (parameters.Count != 3)
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
                int deckID = (int)parameters[(byte)FetchAllDecksResponseParameterCode.DeckID];
                string deckName = (string)parameters[(byte)FetchAllDecksResponseParameterCode.DeckName];
                int maxCardCount = (int)parameters[(byte)FetchAllDecksResponseParameterCode.MaxCardCount];

                subject.LoadDeck(new Deck(deckID, deckName, maxCardCount));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

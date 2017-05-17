using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataResponseParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers.Fetch
{
    class FetchAllDecksHandler : PlayerFetchDataHandler
    {
        public FetchAllDecksHandler(Player subject) : base(subject, 0)
        {
        }

        public override bool Handle(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(fetchCode, parameters, out errorMessage))
            {
                foreach (var deck in subject.Decks)
                {
                    var result = new Dictionary<byte, object>
                        {
                            { (byte)FetchAllDecksResponseParameterCode.DeckID, deck.DeckID },
                            { (byte)FetchAllDecksResponseParameterCode.DeckName, deck.DeckName },
                            { (byte)FetchAllDecksResponseParameterCode.MaxCardCount, deck.MaxCardCount }
                        };
                    SendResponse(fetchCode, ReturnCode.Correct, "", result);
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

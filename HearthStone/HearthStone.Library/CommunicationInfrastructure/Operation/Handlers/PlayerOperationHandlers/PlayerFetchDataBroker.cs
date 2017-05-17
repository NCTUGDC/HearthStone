using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers.Fetch;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataParameters.Player;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    public class PlayerFetchDataBroker : FetchDataBroker<Player, PlayerOperationCode, PlayerFetchDataCode>
    {
        internal PlayerFetchDataBroker(Player subject) : base(subject)
        {
            fetchTable.Add(PlayerFetchDataCode.AllDecks, new FetchAllDecksHandler(subject));
            fetchTable.Add(PlayerFetchDataCode.AllDeckCards, new FetchAllDeckCardsHandler(subject));
        }

        internal override void SendResponse(PlayerOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameter)
        {
            subject.ResponseManager.SendResponse(operationCode, returnCode, operationMessage, parameter);
        }

        internal void SendOperation(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            subject.OperationManager.SendFetchDataOperation(fetchCode, parameters);
        }

        public void FetchAllDecks()
        {
            SendOperation(PlayerFetchDataCode.AllDecks, new Dictionary<byte, object>());
        }
        public void FetchAllDeckCards(int deckID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)FetchAllDeckCardsParameterCode.DeckID, deckID }
            };
            SendOperation(PlayerFetchDataCode.AllDeckCards, parameters);
        }
    }
}

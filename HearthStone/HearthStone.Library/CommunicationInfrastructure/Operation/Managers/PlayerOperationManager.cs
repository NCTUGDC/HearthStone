using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataParameters;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Managers
{
    public class PlayerOperationManager
    {
        private readonly Player player;
        private readonly Dictionary<PlayerOperationCode, OperationHandler<Player, PlayerOperationCode>> operationTable = new Dictionary<PlayerOperationCode, OperationHandler<Player, PlayerOperationCode>>();
        public PlayerFetchDataBroker FetchDataBroker { get; private set; }

        internal PlayerOperationManager(Player player)
        {
            this.player = player;
            FetchDataBroker = new PlayerFetchDataBroker(player);

            operationTable.Add(PlayerOperationCode.FetchData, FetchDataBroker);
            operationTable.Add(PlayerOperationCode.CreateDeck, new CreateDeckHandler(player));
            operationTable.Add(PlayerOperationCode.DeleteDeck, new DeleteDeckHandler(player));
            operationTable.Add(PlayerOperationCode.AddCardToDeck, new AddCardToDeckHandler(player));
            operationTable.Add(PlayerOperationCode.RemoveCardFromDeck, new RemoveCardFromDeckHandler(player));
        }
        internal bool Operate(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (operationTable[operationCode].Handle(operationCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"PlayerOperation Error: {operationCode} from Player: {player.PlayerID}\nErrorMessahe: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow PlayerOperation:{operationCode} from Player: {player.PlayerID}";
                return false;
            }
        }
        internal void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            player.EndPoint.OperationManager.SendPlayerOperation(player, operationCode, parameters);
        }

        internal void SendFetchDataOperation(PlayerFetchDataCode fetchCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> fetchDataParameters = new Dictionary<byte, object>
            {
                { (byte)FetchDataParameterCode.FetchDataCode, (byte)fetchCode },
                { (byte)FetchDataParameterCode.Parameters, parameters }
            };
            SendOperation(PlayerOperationCode.FetchData, fetchDataParameters);
        }

        public void CreateDeck(string deckName)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)CreateDeckParameterCode.DeckName, deckName }
            };
            SendOperation(PlayerOperationCode.CreateDeck, parameters);
        }
        public void DeleteDeck(int deckID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)DeleteDeckParameterCode.DeckID, deckID }
            };
            SendOperation(PlayerOperationCode.DeleteDeck, parameters);
        }
        public void AddCardToDeck(int deckID, int cardID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)AddCardToDeckParameterCode.DeckID, deckID },
                { (byte)AddCardToDeckParameterCode.CardID, cardID }
            };
            SendOperation(PlayerOperationCode.AddCardToDeck, parameters);
        }
        public void RemoveCardFromDeck(int deckID, int cardID)
        {
            Dictionary<byte, object> parameters = new Dictionary<byte, object>
            {
                { (byte)RemoveCardFromDeckParameterCode.DeckID, deckID },
                { (byte)RemoveCardFromDeckParameterCode.CardID, cardID }
            };
            SendOperation(PlayerOperationCode.RemoveCardFromDeck, parameters);
        }
    }
}

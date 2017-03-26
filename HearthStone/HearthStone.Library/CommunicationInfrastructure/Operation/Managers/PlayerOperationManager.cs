using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataParameters;
using HearthStone.Protocol.Communication.OperationCodes;
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
    }
}

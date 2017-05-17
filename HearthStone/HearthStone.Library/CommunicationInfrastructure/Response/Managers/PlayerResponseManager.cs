using HearthStone.Library.CommunicationInfrastructure.Response.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Response.Handlers.PlayerResponseHandlers;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using System;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Managers
{
    public class PlayerResponseManager
    {
        private readonly Player player;
        protected readonly Dictionary<PlayerOperationCode, ResponseHandler<Player, PlayerOperationCode>> operationTable = new Dictionary<PlayerOperationCode, ResponseHandler<Player, PlayerOperationCode>>();

        private event Action onFindOpponentFailed;
        public event Action OnFindOpponentFailed { add { onFindOpponentFailed += value; } remove { onFindOpponentFailed -= value; } }

        internal PlayerResponseManager(Player player)
        {
            this.player = player;

            operationTable.Add(PlayerOperationCode.FetchData, new FetchDataResponseBroker(player));
            operationTable.Add(PlayerOperationCode.FindOpponent, new FindOpponentResponseHandler(player));
        }
        public bool Operate(PlayerOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (operationTable[operationCode].Handle(operationCode, returnCode, operationMessage, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"PlayerResponse Error: {operationCode} from Player: {player.PlayerID}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow PlayerResponse:{operationCode} from Player: {player.PlayerID}";
                return false;
            }
        }
        internal void SendResponse(PlayerOperationCode operationCode, ReturnCode errorCode, string operationMessage, Dictionary<byte, object> parameters)
        {
            player.EndPoint.ResponseManager.SendPlayerResponse(player, operationCode, errorCode, operationMessage, parameters);
        }

        public void FindOpponentFailed()
        {
            onFindOpponentFailed?.Invoke();
        }
    }
}

using HearthStone.Library.CommunicationInfrastructure.Response.Handlers;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Managers
{
    public class PlayerResponseManager
    {
        private readonly Player player;
        protected readonly Dictionary<PlayerOperationCode, ResponseHandler<Player, PlayerOperationCode>> operationTable = new Dictionary<PlayerOperationCode, ResponseHandler<Player, PlayerOperationCode>>();

        public PlayerResponseManager(Player player)
        {
            this.player = player;
            //operationTable.Add();
        }
        public void Operate(PlayerOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, returnCode, debugMessage, parameters))
                {
                    LogService.ErrorFormat($"PlayerResponse Error: {operationCode} from Player: {player.PlayerID}");
                }
            }
            else
            {
                LogService.ErrorFormat($"Unknow PlayerResponse:{operationCode} from Player: {player.PlayerID}");
            }
        }
        public void SendResponse(PlayerOperationCode operationCode, ReturnCode errorCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            player.EndPoint.ResponseManager.SendPlayerResponse(player, operationCode, errorCode, debugMessage, parameters);
        }
    }
}

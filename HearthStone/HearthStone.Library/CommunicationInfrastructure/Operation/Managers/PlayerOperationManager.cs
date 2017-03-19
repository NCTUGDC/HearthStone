using HearthStone.Library.CommunicationInfrastructure.Operation.Handlers;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Managers
{
    public class PlayerOperationManager
    {
        private readonly Player player;
        private readonly Dictionary<PlayerOperationCode, OperationHandler<Player, PlayerOperationCode>> operationTable = new Dictionary<PlayerOperationCode, OperationHandler<Player, PlayerOperationCode>>();

        public PlayerOperationManager(Player player)
        {
            this.player = player;

            //operationTable.Add();
        }
        public void Operate(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            if (operationTable.ContainsKey(operationCode))
            {
                if (!operationTable[operationCode].Handle(operationCode, parameters))
                {
                    LogService.ErrorFormat($"PlayerOperation Error: {operationCode} from Player: {player.PlayerID}");
                }
            }
            else
            {
                LogService.ErrorFormat($"Unknow PlayerOperation:{operationCode} from Player: {player.PlayerID}");
            }
        }
        public void SendOperation(PlayerOperationCode operationCode, Dictionary<byte, object> parameters)
        {
            player.EndPoint.OperationManager.SendPlayerOperation(player, operationCode, parameters);
        }
    }
}

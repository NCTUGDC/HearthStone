using System;
using System.Collections.Generic;
using HearthStone.Library.CommunicationInfrastructure.Event.Handlers;
using HearthStone.Protocol.Communication.EventCodes;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Managers
{
    public class PlayerEventManager
    {
        private readonly Player player;
        private readonly Dictionary<PlayerEventCode, EventHandler<Player, PlayerEventCode>> eventTable = new Dictionary<PlayerEventCode, EventHandler<Player, PlayerEventCode>>();
        internal PlayerEventManager(Player player)
        {
            this.player = player;
        }

        internal void Operate(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (!eventTable[eventCode].Handle(eventCode, parameters))
                {
                    LogService.ErrorFormat($"Player Event Error: {eventCode} from PlayerID: {player.PlayerID}");
                }
            }
            else
            {
                LogService.ErrorFormat($"Unknow Player Event:{eventCode} from PlayerID: {player.PlayerID}");
            }
        }

        internal void SendEvent(PlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            player.EndPoint.EventManager.SendPlayerEvent(player, eventCode, parameters);
        }
    }
}

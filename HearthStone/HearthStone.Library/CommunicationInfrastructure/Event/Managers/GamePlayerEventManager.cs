using HearthStone.Library.CommunicationInfrastructure.Event.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.GamePlayer;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters;
using System;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Managers
{
    public class GamePlayerEventManager
    {
        private readonly GamePlayer gamePlayer;
        private readonly Dictionary<GamePlayerEventCode, EventHandler<GamePlayer, GamePlayerEventCode>> eventTable = new Dictionary<GamePlayerEventCode, EventHandler<GamePlayer, GamePlayerEventCode>>();
        public GamePlayerSyncDataBroker SyncDataBroker { get; private set; }

        public event Action<GamePlayer> OnDrawCard;

        internal GamePlayerEventManager(GamePlayer gamePlayer)
        {
            this.gamePlayer = gamePlayer;
            SyncDataBroker = new GamePlayerSyncDataBroker(gamePlayer);

            eventTable.Add(GamePlayerEventCode.SyncData, SyncDataBroker);
            eventTable.Add(GamePlayerEventCode.DrawCard, new DrawCardHandler(gamePlayer));
        }

        internal bool Operate(GamePlayerEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (eventTable[eventCode].Handle(eventCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"GamePlayer Event Error: {eventCode} from GamePlayerID: {gamePlayer.GamePlayerID}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow GamePlayer Event:{eventCode} from GamePlayerID: {gamePlayer.GamePlayerID}";
                return false;
            }
        }

        internal void SendEvent(GamePlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            gamePlayer.Game.EventManager.SendGamePlayerEvent(gamePlayer, eventCode, parameters);
        }
        internal void SendSyncDataEvent(GamePlayerSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> syncDataParameters = new Dictionary<byte, object>
            {
                { (byte)SyncDataEventParameterCode.SyncDataCode, (byte)syncCode },
                { (byte)SyncDataEventParameterCode.Parameters, parameters }
            };
            SendEvent(GamePlayerEventCode.SyncData, syncDataParameters);
        }
        public void DrawCard(GameDeck deck, int cardRecordID)
        {
            SendEvent(GamePlayerEventCode.DrawCard, new Dictionary<byte, object>());
        }
        internal void DrawCardEvent()
        {
            OnDrawCard?.Invoke(gamePlayer);
        }
    }
}

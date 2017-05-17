using HearthStone.Library.CommunicationInfrastructure.Event.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Managers
{
    public class GameEventManager
    {
        private readonly Game game;
        private readonly Dictionary<GameEventCode, EventHandler<Game, GameEventCode>> eventTable = new Dictionary<GameEventCode, EventHandler<Game, GameEventCode>>();
        public GameSyncDataBroker SyncDataBroker { get; private set; }

        internal GameEventManager(Game game)
        {
            this.game = game;
            SyncDataBroker = new GameSyncDataBroker(game);

            eventTable.Add(GameEventCode.SyncData, SyncDataBroker);
        }

        internal bool Operate(GameEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (eventTable.ContainsKey(eventCode))
            {
                if (eventTable[eventCode].Handle(eventCode, parameters, out errorMessage))
                {
                    return true;
                }
                else
                {
                    errorMessage = $"Player Game Error: {eventCode} from CurrentGamePlayerID: {game.CurrentGamePlayerID}\nErrorMessage: {errorMessage}";
                    return false;
                }
            }
            else
            {
                errorMessage = $"Unknow Game Event:{eventCode} from CurrentGamePlayerID: {game.CurrentGamePlayerID}";
                return false;
            }
        }

        internal void SendEvent(GameEventCode eventCode, Dictionary<byte, object> parameters)
        {
            game.GamePlayer1.Player.EndPoint.EventManager.SendGameEvent(game, eventCode, parameters);
            game.GamePlayer2.Player.EndPoint.EventManager.SendGameEvent(game, eventCode, parameters);
        }
        internal void SendSyncDataEvent(GameSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> syncDataParameters = new Dictionary<byte, object>
            {
                { (byte)SyncDataEventParameterCode.SyncDataCode, (byte)syncCode },
                { (byte)SyncDataEventParameterCode.Parameters, parameters }
            };
            SendEvent(GameEventCode.SyncData, syncDataParameters);
        }
    }
}

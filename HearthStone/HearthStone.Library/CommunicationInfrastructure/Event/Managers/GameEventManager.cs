using HearthStone.Library.CommunicationInfrastructure.Event.Handlers;
using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.Game;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters;
using System;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Managers
{
    public class GameEventManager
    {
        private readonly Game game;
        private readonly Dictionary<GameEventCode, EventHandler<Game, GameEventCode>> eventTable = new Dictionary<GameEventCode, EventHandler<Game, GameEventCode>>();
        public GameSyncDataBroker SyncDataBroker { get; private set; }

        public event Action<Game> OnRoundStart;
        public event Action<Game> OnRoundEnd;
        public event Action<Game, int> OnGameOver;

        internal GameEventManager(Game game)
        {
            this.game = game;
            SyncDataBroker = new GameSyncDataBroker(game);

            eventTable.Add(GameEventCode.SyncData, SyncDataBroker);
            eventTable.Add(GameEventCode.GamePlayerEvent, new GamePlayerEventBroker(game));
            eventTable.Add(GameEventCode.FieldEvent, new FieldEventBroker(game));

            //eventTable.Add(GameEventCode.RoundStart, new RoundStartHandler(game));
            //eventTable.Add(GameEventCode.RoundEnd, new RoundEndHandler(game));
            eventTable.Add(GameEventCode.GameOver, new GameOverHandler(game));
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
        internal void SendGamePlayerEvent(GamePlayer gamePlayer, GamePlayerEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)GamePlayerEventParameterCode.GamePlayerID, gamePlayer.GamePlayerID },
                { (byte)GamePlayerEventParameterCode.EventCode, (byte)eventCode },
                { (byte)GamePlayerEventParameterCode.Parameters, parameters }
            };
            SendEvent(GameEventCode.GamePlayerEvent, eventData);
        }
        internal void SendFieldEvent(Field filed, FieldEventCode eventCode, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FieldEventParameterCode.FieldID, filed.FieldID },
                { (byte)FieldEventParameterCode.EventCode, (byte)eventCode },
                { (byte)FieldEventParameterCode.Parameters, parameters }
            };
            SendEvent(GameEventCode.FieldEvent, eventData);
        }

        //public void RoundStart(Game game)
        //{
        //    SendEvent(GameEventCode.RoundStart, new Dictionary<byte, object>());
        //}
        //public void RoundEnd(Game game)
        //{
        //    SendEvent(GameEventCode.RoundEnd, new Dictionary<byte, object>());
        //}
        public void GameOver(Game game, int winnerGamePlayerID)
        {
            Dictionary<byte, object> syncDataParameters = new Dictionary<byte, object>
            {
                { (byte)GameOverParameterCode.WinnerGamePlayerID, winnerGamePlayerID },
            };
            SendEvent(GameEventCode.GameOver, syncDataParameters);
        }
        internal void RoundStartEvent()
        {
            OnRoundStart?.Invoke(game);
        }
        internal void RoundEndEvent()
        {
            OnRoundEnd?.Invoke(game);
        }
        internal void GameOverEvent(int winnerGamePlayerID)
        {
            OnGameOver?.Invoke(game, winnerGamePlayerID);
        }
    }
}

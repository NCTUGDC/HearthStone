using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Player
{
    class GameStartHandler : EventHandler<Library.Player, PlayerEventCode>
    {
        public GameStartHandler(Library.Player subject) : base(subject, 10)
        {
        }

        internal override bool Handle(PlayerEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(eventCode, parameters, out errorMessage))
            {
                int gameID = (int)parameters[(byte)GameStartParameterCode.GameID];
                int player1ID = (int)parameters[(byte)GameStartParameterCode.Player1ID];
                string player1Nickname = (string)parameters[(byte)GameStartParameterCode.Player1Nickname];
                Library.GamePlayer gamePlayer1 = SerializationHelper.Deserialize<Library.GamePlayer>((byte[])parameters[(byte)GameStartParameterCode.GamePlayer1DataByteArray]);
                int player2ID = (int)parameters[(byte)GameStartParameterCode.Player2ID];
                string player2Nickname = (string)parameters[(byte)GameStartParameterCode.Player2Nickname];
                Library.GamePlayer gamePlayer2 = SerializationHelper.Deserialize<Library.GamePlayer>((byte[])parameters[(byte)GameStartParameterCode.GamePlayer2DataByteArray]);
                int roundCount = (int)parameters[(byte)GameStartParameterCode.RoundCount];
                int currentGamePlayerID = (int)parameters[(byte)GameStartParameterCode.CurrentGamePlayerID];
                GameCardManager gameCardManager = SerializationHelper.Deserialize<GameCardManager>((byte[])parameters[(byte)GameStartParameterCode.GameCardManagerByteArray]);

                gamePlayer1.Player = new Library.Player(player1ID, player1Nickname);
                gamePlayer2.Player = new Library.Player(player2ID, player2Nickname);

                Library.Game game = new Library.Game
                (
                    gameID: gameID,
                    gamePlayer1: gamePlayer1,
                    gamePlayer2: gamePlayer2,
                    roundCount: roundCount,
                    currentGamePlayerID: currentGamePlayerID,
                    gameCardManager: gameCardManager
                );
                GameManager.Instance.AddGame(game);
                gamePlayer1.BindGame(game);
                gamePlayer2.BindGame(game);
                gameCardManager.BindGame(game);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

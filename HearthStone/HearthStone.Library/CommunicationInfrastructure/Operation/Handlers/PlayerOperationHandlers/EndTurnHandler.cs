using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class EndTurnHandler : PlayerOperationHandler
    {
        public EndTurnHandler(Player subject) : base(subject, 1)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                int gameID = (int)parameters[(byte)EndTurnParameterCode.GameID];

                Game game;
                if (GameManager.Instance.FindGame(gameID, out game))
                {
                    int gamePlayerID = game.SelectGamePlayerID(subject.PlayerID);
                    if(gamePlayerID == 1 && game.CurrentGamePlayerID == 1)
                    {
                        game.EndRound();
                        return true;
                    }
                    else if (gamePlayerID == 2 && game.CurrentGamePlayerID == 2)
                    {
                        game.EndRound();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class SwapHandsHandler : PlayerOperationHandler
    {
        public SwapHandsHandler(Player subject) : base(subject, 2)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                int gameID = (int)parameters[(byte)SwapHandsParameterCode.GameID];
                int[] swapCardRecordID_Array = (int[])parameters[(byte)SwapHandsParameterCode.SwapCardRecordID_Array];

                Game game;
                if (GameManager.Instance.FindGame(gameID, out game))
                {
                    int gamePlayerID = game.SelectGamePlayerID(subject.PlayerID);
                    if(gamePlayerID > 0)
                    {
                        if(gamePlayerID == 1)
                        {
                            game.GamePlayer1.ChangeHand(swapCardRecordID_Array);
                        }
                        else
                        {
                            game.GamePlayer2.ChangeHand(swapCardRecordID_Array);
                        }
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

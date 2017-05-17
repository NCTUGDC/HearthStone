using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.PlayerResponseHandlers
{
    class FindOpponentResponseHandler : ResponseHandler<Player, PlayerOperationCode>
    {
        public FindOpponentResponseHandler(Player subject) : base(subject, 0)
        {
        }

        internal override bool CheckError(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage)
        {
            if(!base.CheckError(operationCode, parameters, returnCode, operationMessage, out errorMessage))
            {
                switch(returnCode)
                {
                    case ReturnCode.InvalidOperation:
                        subject.ResponseManager.FindOpponentFailed();
                        return true;
                    default:
                        return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}

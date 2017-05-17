using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataResponseParameters;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    internal abstract class PlayerFetchDataHandler : FetchDataHandler<Player, PlayerFetchDataCode>
    {
        protected PlayerFetchDataHandler(Player subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        public override void SendResponse(PlayerFetchDataCode fetchCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FetchDataResponseParameterCode.FetchCode, (byte)fetchCode },
                { (byte)FetchDataResponseParameterCode.ReturnCode, (short)returnCode },
                { (byte)FetchDataResponseParameterCode.OperationMessage, null },
                { (byte)FetchDataResponseParameterCode.Parameters, parameters }
            };
            subject.ResponseManager.SendResponse(PlayerOperationCode.FetchData, ReturnCode.Correct, "", eventData);
        }
    }
}

using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.FetchDataResponseParameters;
using HearthStone.Protocol.Communication.OperationCodes;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.EndPointOperationHandlers
{
    internal abstract class EndPointFetchDataHandler : FetchDataHandler<EndPoint, EndPointFetchDataCode>
    {
        protected EndPointFetchDataHandler(EndPoint subject, int correctParameterCount) : base(subject, correctParameterCount)
        {
        }

        public override void SendResponse(EndPointFetchDataCode fetchCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)FetchDataResponseParameterCode.FetchCode, (byte)fetchCode },
                { (byte)FetchDataResponseParameterCode.ReturnCode, (short)returnCode },
                { (byte)FetchDataResponseParameterCode.OperationMessage, null },
                { (byte)FetchDataResponseParameterCode.Parameters, parameters }
            };
            subject.ResponseManager.SendResponse(EndPointOperationCode.FetchData, ReturnCode.Correct, "", eventData);
        }
    }
}

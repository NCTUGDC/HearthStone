using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataParameters;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers
{
    public abstract class FetchDataBroker<TSubject, TOperationCode, TFetchDataCode> : OperationHandler<TSubject, TOperationCode>
    {
        protected readonly Dictionary<TFetchDataCode, FetchDataHandler<TSubject, TFetchDataCode>> fetchTable;

        public FetchDataBroker(TSubject subject) : base(subject, 2)
        {
            fetchTable = new Dictionary<TFetchDataCode, FetchDataHandler<TSubject, TFetchDataCode>>();
        }

        internal override bool Handle(TOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                TFetchDataCode fetchCode = (TFetchDataCode)parameters[(byte)FetchDataParameterCode.FetchDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataParameterCode.Parameters];
                if (fetchTable.ContainsKey(fetchCode))
                {
                    return fetchTable[fetchCode].Handle(fetchCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"{subject.GetType()} Fetch Operation Not Exist Fetch Code: {fetchCode}";
                    SendResponse(operationCode, ReturnCode.UndefinedOperation, errorMessage, new Dictionary<byte, object>());
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

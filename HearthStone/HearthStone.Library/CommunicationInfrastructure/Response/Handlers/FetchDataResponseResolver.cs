using HearthStone.Protocol;
using HearthStone.Protocol.Communication.FetchDataResponseParameters;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers
{
    public class FetchDataResponseResolver<TSubject, TOperationCode, TFetchDataCode> : ResponseHandler<TSubject, TOperationCode>
    {
        protected readonly Dictionary<TFetchDataCode, FetchDataResponseHandler<TSubject, TFetchDataCode>> fetchResponseTable;

        public FetchDataResponseResolver(TSubject subject) : base(subject, 4)
        {
            fetchResponseTable = new Dictionary<TFetchDataCode, FetchDataResponseHandler<TSubject, TFetchDataCode>>();
        }

        internal override bool CheckError(TOperationCode operationCode, Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage)
        {
            if(base.CheckError(operationCode, parameters, returnCode, operationMessage, out errorMessage))
            {
                return true;
            }
            else
            {
                switch (returnCode)
                {
                    default:
                        return false;
                }
            }
        }

        internal override bool Handle(TOperationCode operationCode, ReturnCode returnCode, string debugMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, returnCode, debugMessage, parameters, out errorMessage))
            {
                TFetchDataCode fetchCode = (TFetchDataCode)parameters[(byte)FetchDataResponseParameterCode.FetchCode];
                ReturnCode resolvedReturnCode = (ReturnCode)parameters[(byte)FetchDataResponseParameterCode.ReturnCode];
                string resolvedOperationMessage = (string)parameters[(byte)FetchDataResponseParameterCode.OperationMessage];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FetchDataResponseParameterCode.Parameters];
                if (fetchResponseTable.ContainsKey(fetchCode))
                {
                    return fetchResponseTable[fetchCode].Handle(fetchCode, resolvedReturnCode, resolvedOperationMessage, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"{subject.GetType()} FetchData Response Not Exist Fetch Code: {fetchCode}, Identity: {subject.ToString()}";
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

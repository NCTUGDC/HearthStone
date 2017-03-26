using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.OperationCodes;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.EndPointResponseHandlers
{
    internal class FetchDataResponseBroker : FetchDataResponseResolver<EndPoint, EndPointOperationCode, EndPointFetchDataCode>
    {
        internal FetchDataResponseBroker(EndPoint subject) : base(subject)
        {
        }
    }
}

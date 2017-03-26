using HearthStone.Protocol.Communication.FetchDataCodes;
using HearthStone.Protocol.Communication.OperationCodes;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.PlayerResponseHandlers
{
    internal class FetchDataResponseBroker : FetchDataResponseResolver<Player, PlayerOperationCode, PlayerFetchDataCode>
    {
        internal FetchDataResponseBroker(Player subject) : base(subject)
        {
        }
    }
}

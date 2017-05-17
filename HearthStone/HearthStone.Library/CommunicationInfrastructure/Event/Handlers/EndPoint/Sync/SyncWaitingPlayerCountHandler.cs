using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.EndPoint;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.EndPoint.Sync
{
    class SyncWaitingPlayerCountHandler :  SyncDataHandler<Library.EndPoint, EndPointSyncDataCode>
    {
        public SyncWaitingPlayerCountHandler(Library.EndPoint subject) : base(subject, 1)
        {
        }
        internal override bool Handle(EndPointSyncDataCode syncCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(syncCode, parameters, out errorMessage))
            {
                int playerCount = (int)parameters[(byte)SyncWaitingPlayerCountParameterCode.PlayerCount];
                WaitingPlayerCounter.WaitingPlayerCount = playerCount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

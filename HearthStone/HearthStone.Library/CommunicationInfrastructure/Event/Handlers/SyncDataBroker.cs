using HearthStone.Protocol.Communication.SyncDataParameters;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers
{
    public abstract class SyncDataResolver<TSubject, TEventCode, TSyncDataCode> : EventHandler<TSubject, TEventCode>
    {
        internal readonly Dictionary<TSyncDataCode, SyncDataHandler<TSubject, TSyncDataCode>> syncTable;

        protected SyncDataResolver(TSubject subject) : base(subject, 2)
        {
            syncTable = new Dictionary<TSyncDataCode, SyncDataHandler<TSubject, TSyncDataCode>>();
        }

        internal override bool Handle(TEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                TSyncDataCode syncCode = (TSyncDataCode)parameters[(byte)SyncDataEventParameterCode.SyncDataCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)SyncDataEventParameterCode.Parameters];
                if (syncTable.ContainsKey(syncCode))
                {
                    return syncTable[syncCode].Handle(syncCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"{subject.GetType()} SyncData Event Not Exist SyncCode: {syncCode}";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        internal abstract void SendSyncData(TSyncDataCode syncCode, Dictionary<byte, object> parameters);
    }
}

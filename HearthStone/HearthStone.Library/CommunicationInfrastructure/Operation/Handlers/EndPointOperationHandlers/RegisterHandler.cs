using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.EndPoint;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.EndPointOperationHandlers
{
    class RegisterHandler : EndPointOperationHandler
    {
        public RegisterHandler(EndPoint subject) : base(subject, 3)
        {
        }
        internal override bool Handle(EndPointOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(operationCode, parameters, out errorMessage))
            {
                string account = (string)parameters[(byte)RegisterParameterCode.Account];
                string password = (string)parameters[(byte)RegisterParameterCode.Password];
                string nickname = (string)parameters[(byte)RegisterParameterCode.Nickname];

                ReturnCode returnCode;
                if(subject.OperationInterface.Register(subject.LastConnectedIPAddress, account, password, nickname, out returnCode, out errorMessage))
                {
                    SendResponse(operationCode, returnCode, errorMessage, new Dictionary<byte, object>());
                    return true;
                }
                else
                {
                    SendResponse(operationCode, returnCode, errorMessage, new Dictionary<byte, object>());
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

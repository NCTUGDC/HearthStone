using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.ResponseParameters.EndPoint;
using System.Collections.Generic;
using System.Net;

namespace HearthStone.Library.CommunicationInfrastructure.Response.Handlers.EndPointResponseHandlers
{
    class LoginResponseHandler : ResponseHandler<EndPoint, EndPointOperationCode>
    {
        public LoginResponseHandler(EndPoint subject) : base(subject, 4)
        {
        }

        internal override bool Handle(EndPointOperationCode operationCode, ReturnCode returnCode, string operationMessage, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if(base.Handle(operationCode, returnCode, operationMessage, parameters, out errorMessage))
            {
                int playerID = (int)parameters[(byte)LoginResponseParameterCode.PlayerID];
                IPAddress lastConnectedIPAddress = IPAddress.Parse((string)parameters[(byte)LoginResponseParameterCode.LastConnectedIPAddress]);
                string account = (string)parameters[(byte)LoginResponseParameterCode.Account];
                string nickname = (string)parameters[(byte)LoginResponseParameterCode.Nickname];

                subject.PlayerOnline(new Player(playerID, lastConnectedIPAddress, account, nickname));
                subject.Player.OperationManager.FetchDataBroker.FetchAllDecks();

                return true;
            }
            else
            {
                return false;
            }
        }
        internal override bool CheckError(EndPointOperationCode operationCode, Dictionary<byte, object> parameters, ReturnCode returnCode, string operationMessage, out string errorMessage)
        {
            if (base.CheckError(operationCode, parameters, returnCode, operationMessage, out errorMessage))
            {
                subject.ResponseManager.LoginResponse(returnCode, operationMessage);
                return true;
            }
            else
            {
                subject.ResponseManager.LoginResponse(returnCode, operationMessage);
                return false;
            }
        }
    }
}

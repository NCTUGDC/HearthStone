using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.EndPoint;
using HearthStone.Protocol.Communication.ResponseParameters.EndPoint;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.EndPointOperationHandlers
{
    class LoginHandler : EndPointOperationHandler
    {
        public LoginHandler(EndPoint subject) : base(subject, 2)
        {
        }
        internal override bool Handle(EndPointOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                string account = (string)parameters[(byte)LoginParameterCode.Account];
                string password = (string)parameters[(byte)LoginParameterCode.Password];

                ReturnCode returnCode;
                Player player;
                if (subject.OperationInterface.Login(account, password,out returnCode, out errorMessage, out player))
                {
                    subject.PlayerOnline(player);
                    Dictionary<byte, object> responseParameter = new Dictionary<byte, object>
                    {
                        { (byte)LoginResponseParameterCode.PlayerID, player.PlayerID },
                        { (byte)LoginResponseParameterCode.LastConnectedIPAddress, player.LastConnectedIPAddress.ToString() },
                        { (byte)LoginResponseParameterCode.Account, player.Account },
                        { (byte)LoginResponseParameterCode.Nickname, player.Nickname }
                    };
                    SendResponse(operationCode, returnCode, errorMessage, responseParameter);
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

using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.EventParameters.Game;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Game
{
    public class FieldEventBroker : EventHandler<Library.Game, GameEventCode>
    {
        public FieldEventBroker(Library.Game subject) : base(subject, 3)
        {
        }

        internal override bool Handle(GameEventCode eventCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(eventCode, parameters, out errorMessage))
            {
                int fieldID = (int)parameters[(byte)FieldEventParameterCode.FieldID];
                FieldEventCode resolvedEventCode = (FieldEventCode)parameters[(byte)FieldEventParameterCode.EventCode];
                Dictionary<byte, object> resolvedParameters = (Dictionary<byte, object>)parameters[(byte)FieldEventParameterCode.Parameters];
                
                if (fieldID == 1)
                {
                    return subject.Field1.EventManager.Operate(resolvedEventCode, resolvedParameters, out errorMessage);
                }
                else if(fieldID == 2)
                {
                    return subject.Field2.EventManager.Operate(resolvedEventCode, resolvedParameters, out errorMessage);
                }
                else
                {
                    errorMessage = $"FiledEvent Error Field ID: {fieldID} Not in Game: {subject.GameID}";
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

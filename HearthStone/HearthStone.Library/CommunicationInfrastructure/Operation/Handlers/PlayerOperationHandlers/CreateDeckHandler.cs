using System;
using System.Collections.Generic;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationParameters.PlayerParameterCodes;
using HearthStone.Protocol.Communication.OperationCodes;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class CreateDeckHandler : PlayerOperationHandler
    {
        public CreateDeckHandler(Player subject) : base(subject, 1)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                string deckName = (string)parameters[(byte)CreateDeckParameterCode.DeckName];

                ReturnCode returnCode;
                Deck deck;
                if (subject.EndPoint.OperationInterface.CreateDeck(subject.PlayerID, deckName, out returnCode, out errorMessage, out deck))
                {
                    subject.LoadDeck(deck);
                    return true;
                }
                else
                {
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

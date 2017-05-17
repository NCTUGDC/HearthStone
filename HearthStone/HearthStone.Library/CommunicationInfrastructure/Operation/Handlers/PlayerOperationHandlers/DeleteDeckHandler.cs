using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class DeleteDeckHandler : PlayerOperationHandler
    {
        public DeleteDeckHandler(Player subject) : base(subject, 1)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                int deckID = (int)parameters[(byte)DeleteDeckParameterCode.DeckID];

                ReturnCode returnCode;
                Deck deck;
                if(subject.FindDeck(deckID, out deck))
                {
                    if (subject.EndPoint.OperationInterface.DeleteDeck(deckID, out returnCode, out errorMessage))
                    {
                        subject.RemoveDeck(deckID);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    errorMessage = "Deck Not Existed";
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

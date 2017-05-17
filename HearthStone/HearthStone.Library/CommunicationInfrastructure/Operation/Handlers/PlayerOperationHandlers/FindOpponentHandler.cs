using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class FindOpponentHandler : PlayerOperationHandler
    {
        public FindOpponentHandler(Player subject) : base(subject, 1)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                int deckID = (int)parameters[(byte)FindOpponentParameterCode.DeckID];

                Deck deck;
                if (subject.FindDeck(deckID, out deck) && deck.IsCompleted)
                {
                    subject.EndPoint.OperationInterface.FindOpponent(subject, deck);
                    return true;
                }
                else
                {
                    SendResponse(operationCode, Protocol.ReturnCode.InvalidOperation, "不合法的牌組", new Dictionary<byte, object>());
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

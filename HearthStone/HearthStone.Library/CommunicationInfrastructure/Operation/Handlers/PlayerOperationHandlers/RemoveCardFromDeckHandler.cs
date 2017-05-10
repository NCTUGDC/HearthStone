using HearthStone.Protocol.Communication.OperationCodes;
using HearthStone.Protocol.Communication.OperationParameters.Player;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class RemoveCardFromDeckHandler : PlayerOperationHandler
    {
        public RemoveCardFromDeckHandler(Player subject) : base(subject, 2)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                int deckID = (int)parameters[(byte)RemoveCardFromDeckParameterCode.DeckID];
                int cardID = (int)parameters[(byte)RemoveCardFromDeckParameterCode.CardID];
                
                Deck deck;
                Card card;
                if (subject.FindDeck(deckID, out deck) && CardManager.Instance.FindCard(cardID, out card) && deck.CardCount(card.CardID) > 0)
                {
                    return deck.RemoveCard(card.CardID); ;
                }
                else
                {
                    errorMessage = "Deck or Card Not Exist";
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

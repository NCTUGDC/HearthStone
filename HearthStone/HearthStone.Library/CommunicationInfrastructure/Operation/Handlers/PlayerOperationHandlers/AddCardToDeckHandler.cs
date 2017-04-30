using System;
using System.Collections.Generic;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.OperationParameters.PlayerParameterCodes;
using HearthStone.Protocol.Communication.OperationCodes;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class AddCardToDeckHandler : PlayerOperationHandler
    {
        public AddCardToDeckHandler(Player subject) : base(subject, 2)
        {
        }

        internal override bool Handle(PlayerOperationCode operationCode, Dictionary<byte, object> parameters, out string errorMessage)
        {
            if (base.Handle(operationCode, parameters, out errorMessage))
            {
                int deckID = (int)parameters[(byte)AddCardToDeckParameterCode.DeckID];
                int cardID = (int)parameters[(byte)AddCardToDeckParameterCode.CardID];

                Deck deck;
                Card card;
                if (subject.FindDeck(deckID, out deck) && CardManager.Instance.FindCard(cardID, out card))
                {
                    deck.AddCard(card);
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

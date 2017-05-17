using HearthStone.Protocol;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.Player;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Player.Sync;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.Player
{
    public class PlayerSyncDataBroker : SyncDataResolver<Library.Player, PlayerEventCode, PlayerSyncDataCode>
    {
        internal PlayerSyncDataBroker(Library.Player subject) : base(subject)
        {
            syncTable.Add(PlayerSyncDataCode.DeckChanged, new SyncDeckChangedHandler(subject));
            syncTable.Add(PlayerSyncDataCode.DeckCardChanged, new SyncDeckCardChangedHandler(subject));
        }

        internal override void SendSyncData(PlayerSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }

        public void SyncDeckChanged(Deck deck, DataChangeCode changeCode)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncDeckChangedParameterCode.DataChangeCode, (byte)changeCode },
                { (byte)SyncDeckChangedParameterCode.DeckID, deck.DeckID },
                { (byte)SyncDeckChangedParameterCode.DeckName, deck.DeckName },
                { (byte)SyncDeckChangedParameterCode.MaxCardCount, deck.MaxCardCount }
            };
            SendSyncData(PlayerSyncDataCode.DeckChanged, eventData);
        }
        public void SyncDeckCardChanged(int deckID, int cardID, DataChangeCode changeCode)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncDeckCardChangedParameterCode.DataChangeCode, (byte)changeCode },
                { (byte)SyncDeckCardChangedParameterCode.DeckID, deckID },
                { (byte)SyncDeckCardChangedParameterCode.CardID, cardID }
            };
            SendSyncData(PlayerSyncDataCode.DeckCardChanged, eventData);
        }
    }
}

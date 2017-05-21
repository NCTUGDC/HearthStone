using HearthStone.Library.CommunicationInfrastructure.Event.Handlers.GamePlayer.Sync;
using HearthStone.Protocol;
using HearthStone.Protocol.Communication.EventCodes;
using HearthStone.Protocol.Communication.SyncDataCodes;
using HearthStone.Protocol.Communication.SyncDataParameters.GamePlayer;
using System.Collections.Generic;

namespace HearthStone.Library.CommunicationInfrastructure.Event.Handlers.GamePlayer
{
    public class GamePlayerSyncDataBroker : SyncDataResolver<Library.GamePlayer, GamePlayerEventCode, GamePlayerSyncDataCode>
    {
        internal GamePlayerSyncDataBroker(Library.GamePlayer subject) : base(subject)
        {
            syncTable.Add(GamePlayerSyncDataCode.HasChangedHandChanged, new SyncHasChangedHandChangedHandler(subject));
            syncTable.Add(GamePlayerSyncDataCode.HandCardsChanged, new SyncHandCardsChangedHandler(subject));
            syncTable.Add(GamePlayerSyncDataCode.RemainedManaCrystalChanged, new SyncRemainedManaCrystalChangedHandler(subject));
            syncTable.Add(GamePlayerSyncDataCode.ManaCrystalChanged, new SyncManaCrystalChangedHandler(subject));

            syncTable.Add(GamePlayerSyncDataCode.DeckCardsChanged, new SyncDeckCardsChangedHandler(subject));
        }

        internal override void SendSyncData(GamePlayerSyncDataCode syncCode, Dictionary<byte, object> parameters)
        {
            subject.EventManager.SendSyncDataEvent(syncCode, parameters);
        }

        public void SyncHasChangedHandChanged(Library.GamePlayer gamePlayer)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncHasChangedHandChangedParameterCode.HasChangedHand, gamePlayer.HasChangedHand }
            };
            SendSyncData(GamePlayerSyncDataCode.HasChangedHandChanged, eventData);
        }
        public void SyncHandCardsChanged(Library.GamePlayer gamePlayer, int cardRecordID, DataChangeCode changeCode)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncHandCardsChangedParameterCode.DataChangeCode, (byte)changeCode },
                { (byte)SyncHandCardsChangedParameterCode.CardRecordID, cardRecordID }
            };
            SendSyncData(GamePlayerSyncDataCode.HandCardsChanged, eventData);
        }
        public void SyncRemainedManaCrystalChanged(Library.GamePlayer gamePlayer)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncRemainedManaCrystalChangedParameterCode.RemainedManaCrystal, gamePlayer.RemainedManaCrystal }
            };
            SendSyncData(GamePlayerSyncDataCode.RemainedManaCrystalChanged, eventData);
        }
        public void SyncManaCrystalChanged(Library.GamePlayer gamePlayer)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncManaCrystalChangedParameterCode.ManaCrystal, gamePlayer.ManaCrystal }
            };
            SendSyncData(GamePlayerSyncDataCode.ManaCrystalChanged, eventData);
        }
        public void SyncDeckCardsChanged(GameDeck deck, int cardRecordID, DataChangeCode changeCode)
        {
            Dictionary<byte, object> eventData = new Dictionary<byte, object>
            {
                { (byte)SyncDeckCardsChangedParameterCode.DataChangeCode, (byte)changeCode },
                { (byte)SyncDeckCardsChangedParameterCode.CardRecordID, cardRecordID }
            };
            SendSyncData(GamePlayerSyncDataCode.DeckCardsChanged, eventData);
        }
    }
}

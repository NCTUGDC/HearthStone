namespace HearthStone.Protocol.Communication.EventCodes
{
    public enum GameEventCode : byte
    {
        SyncData,
        GamePlayerEvent,
        FieldEvent,

        RoundStart,
        RoundEnd,
        GameOver,

        CardRecordDestroyed
    }
}

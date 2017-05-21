namespace HearthStone.Protocol.Communication.SyncDataCodes
{
    public enum GameSyncDataCode : byte
    {
        RoundCountChanged,
        CurrentGamePlayerID_Changed,

        CardRecordChanged,
        EffectorChanged,

        CardRecordManaCostChanged,
        CardRecordEffectorChanged,
        CardRecordIsDisplayInThisTurnChanged,
        CardRecordAttackCountInThisTurnChanged,

        ServantCardRecordAttackChanged,
        ServantCardRecordHealthChanged,
        ServantCardRecordRemainedHealthChanged,

        WeaponCardRecordAttackChanged,
        WeaponCardRecordDurabilityChanged,
        WeaponCardRecordRemainedDurabilityChanged
    }
}

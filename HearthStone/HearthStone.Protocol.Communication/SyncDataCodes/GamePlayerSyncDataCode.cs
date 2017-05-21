namespace HearthStone.Protocol.Communication.SyncDataCodes
{
    public enum GamePlayerSyncDataCode : byte
    {
        HasChangedHandChanged,
        HandCardsChanged,
        RemainedManaCrystalChanged,
        ManaCrystalChanged,

        DeckCardsChanged,

        HeroWeaponChanged,
        HeroAttackChanged,
        HeroRemainedHP_Changed,
        HeroHP_Changed,
        HeroEffectorChanged,
        HeroAttackCountInThisTurnChanged
    }
}

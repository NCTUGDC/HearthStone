namespace HearthStone.Protocol.Communication.OperationCodes
{
    public enum PlayerOperationCode : byte
    {
        FetchData,
        CreateDeck,
        DeleteDeck,
        AddCardToDeck,
        RemoveCardFromDeck,
        FindOpponent,

        SwapHands,
        TargetDisplayServant,
        NonTargetDisplayServant,
        TargetCastSpell,
        NonTargeCasttSpell,
        EquipWeapon,
        ServantAttackHero,
        ServantAttackServant,
        HeroAttackHero,
        HeroAttackServant,
        EndTurn
    }
}

namespace HearthStone.Protocol.Communication.OperationCodes
{
    public enum PlayerOperationCode : byte
    {
        FetchData,
        CreateDeck,
        DeleteDeck,
        AddCardToDeck,
        RemoveCardFromDeck
    }
}

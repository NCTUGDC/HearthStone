namespace HearthStone.Protocol.Communication.EventParameters.Player
{
    public enum GameStartParameterCode : byte
    {
        GameID,
        Player1ID,
        Player1Nickname,
        GamePlayer1DataByteArray,
        Player2ID,
        Player2Nickname,
        GamePlayer2DataByteArray,
        RoundCount,
        CurrentGamePlayerID
    }
}

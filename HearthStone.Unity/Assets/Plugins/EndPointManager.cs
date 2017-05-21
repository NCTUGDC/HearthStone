using HearthStone.Library;

public static class EndPointManager
{
    public static EndPoint EndPoint { get; private set; }

    static EndPointManager()
    {
        EndPoint = new EndPoint(new PhotonUnityCommunicationInterface(), null);
        EndPoint.OnPlayerOnline += SetPlayer;
    }
    static void SetPlayer(Player player)
    {
        PlayerManager.Player = player;
    }
}

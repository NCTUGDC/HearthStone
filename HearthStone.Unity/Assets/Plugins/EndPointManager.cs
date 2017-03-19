using HearthStone.Library;

public class EndPointManager
{
    public static EndPoint EndPoint { get; private set; }

    static EndPointManager()
    {
        EndPoint = new EndPoint(new PhotonUnityCommunicationInterface());
    }
}

using HearthStone.Library;
using System.Net;

namespace HearthStone.Database.Repositories
{
    public abstract class PlayerRepository
    {
        public abstract bool Register(IPAddress lastConnectedIPAddress, string account, string passwordHash, string nickname);
        public abstract bool Contains(string account, out int playerID);
        public abstract bool Read(int playerID, out Player player);
        public abstract void Save(Player player);
        public abstract bool LoginCheck(string account, string password);
    }
}

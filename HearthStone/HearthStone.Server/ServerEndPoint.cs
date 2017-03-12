using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthStone.Library;

namespace HearthStone.Server
{
    public class ServerEndPoint : EndPoint
    {
        public Guid Guid { get; protected set; }

        public ServerEndPoint()
        {
            Guid = Guid.NewGuid();
            while (EndPointFactory.Instance.ContainsEndPointGuid(Guid))
            {
                Guid = Guid.NewGuid();
            }
        }
    }
}

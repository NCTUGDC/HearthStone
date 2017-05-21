using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class TargetDisplayServantHandler : PlayerOperationHandler
    {
        public TargetDisplayServantHandler(Player subject) : base(subject, 4)
        {
        }
    }
}

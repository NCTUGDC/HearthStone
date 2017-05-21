using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class EndTurnHandler : PlayerOperationHandler
    {
        public EndTurnHandler(Player subject) : base(subject, 1)
        {
        }
    }
}

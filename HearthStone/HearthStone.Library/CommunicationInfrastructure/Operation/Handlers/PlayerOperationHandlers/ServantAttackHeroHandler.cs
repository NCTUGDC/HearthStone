using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class ServantAttackHeroHandler : PlayerOperationHandler
    {
        public ServantAttackHeroHandler(Player subject) : base(subject, 2)
        {
        }
    }
}

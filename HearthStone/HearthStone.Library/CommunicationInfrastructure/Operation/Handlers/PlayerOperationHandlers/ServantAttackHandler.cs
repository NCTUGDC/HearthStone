﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class ServantAttackHandler : PlayerOperationHandler
    {
        public ServantAttackHandler(Player subject) : base(subject, 2)
        {
        }
    }
}

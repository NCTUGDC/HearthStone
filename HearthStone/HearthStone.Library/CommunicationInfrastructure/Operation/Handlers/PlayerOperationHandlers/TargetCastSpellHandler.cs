﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class TargetCastSpellHandler : PlayerOperationHandler
    {
        public TargetCastSpellHandler(Player subject) : base(subject, 3)
        {
        }
    }
}

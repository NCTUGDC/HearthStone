﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HearthStone.Library.CommunicationInfrastructure.Operation.Handlers.PlayerOperationHandlers
{
    class HeroAttackHandler : PlayerOperationHandler
    {
        public HeroAttackHandler(Player subject) : base(subject, 1)
        {
        }
    }
}

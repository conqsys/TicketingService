﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Entities
{
    public interface IWorklistSchedule : IEntity, IStamp
    {
        int DayOfWeek { get; set; }

        long WorklistId { get; set; }
    }
}

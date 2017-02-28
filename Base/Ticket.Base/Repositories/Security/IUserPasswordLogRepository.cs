﻿using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface IUserPasswordLogRepository : IDepRepository
    {
        Task<IUserPasswordLog> AddNew(IUserPasswordLog entity);

        Task<bool> ValidateUserPassword(long userId, string password);
    }
}

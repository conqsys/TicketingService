using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Repositories
{
    public interface IUserAuthenticationRepository : IDepRepository
    {
        Task<bool> AddNew(long userId, string ipAddress, string email, string phone);

        Task<bool> VerifiyAuthentication(long userId, string ipAddress, string verfifiedCode);

        Task<IUserAuthentication> AddNew(IUserAuthentication entity);
        
        Task<IUserAuthentication> FindByIPAddress(string ipAddress, long userId);

        Task<bool> ValidateIPAddress(string ipAddress, long userId);
    }
}

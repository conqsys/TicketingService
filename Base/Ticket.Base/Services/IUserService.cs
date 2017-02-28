using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Base.Services
{
    public interface IUserService
    {

        Task<IUser> AddNewUser(IUser entity);

        Task ChangePassword(long userId, string password);

        Task<bool> ChangeActiveState(long userId, bool isEnabled, long modifiredById);

        Task<IUser> CheckUserAuthentication(string userName, string ipAddress);

        Task<bool> UpdateUser(IUser entity);

        Task<bool> LogOutUser(long userId);

        Task DeleteRole(long roleId);

        Task<IPaginationQuery<IUser>> GetAllUsers();
    }
}

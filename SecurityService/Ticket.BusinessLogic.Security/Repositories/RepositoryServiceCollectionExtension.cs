using Ticket.Base.Repositories;
using Ticket.BusinessLogic.Common;
using Ticket.BusinessLogic.Security;
using Ticket.DataAccess.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.DataAccess.TicketService;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SecurityServiceRepositoryCollectionExtension
    {
        public static IServiceCollection AddSecurityServiceRepositories(this IServiceCollection services)
        {
          
            services.AddScoped<IUserRepository, UserRepository<User,Role,Client>>();
            services.AddScoped<IRoleRepository, RoleRepository<Role>>();
            services.AddScoped<IUserAuthenticationRepository, UserAuthenticationRepository<UserAuthentication>>();
            services.AddScoped<IUserPasswordLogRepository, UserPasswordLogRepository<UserPasswordLog, User>>();
            services.AddScoped<IGroupUserRepository, GroupUserRepository<GroupUser>>();
            return services;
        }
    }
}

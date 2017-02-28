using Ticket.Base.Repositories;
using Ticket.BusinessLogic.TicketService;
using Ticket.DataAccess.TicketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DocumentServiceRepositoryCollectionExtension
    {
        public static IServiceCollection AddAssetServiceRepositories(this IServiceCollection services)
        {
            /* use interface and implementation if want to resolve dependency in different application*/
            
            services.AddScoped<IRequestTypeRepository, RequestTypeRepository<RequestType>>();
            services.AddScoped<ITicketStatusRepository, TicketStatusRepository<TicketStatus>>();
            services.AddScoped<IFacilityRepository, FacilityRepository<Facility>>();
            services.AddScoped<IProcessRepository, ProcessRepository<Process>>();
            services.AddScoped<IClientRepository, ClientRepository<Client>>();
            services.AddScoped<IWorkOrderRepository, WorkOrderRepository<WorkOrder>>();
            return services;
        }

    }
}

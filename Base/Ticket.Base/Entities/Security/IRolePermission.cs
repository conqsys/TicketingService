using System;

namespace Ticket.Base.Entities
{
    public interface IRolePermission : IEntity, IStamp
    {
        long? RoleID { get; set; }

        long? PermissionID { get; set; }

        string Action { get; set; }

        string Description { get; set; }
    }
}

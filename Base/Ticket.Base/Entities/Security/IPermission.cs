using System;

namespace Ticket.Base.Entities
{
    public interface IPermission : IEntity, IStamp
    {
        string Action { get; set; }

        string Description { get; set; }
    }
}

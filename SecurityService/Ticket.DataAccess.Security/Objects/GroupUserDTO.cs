using Ticket.Base.Entities;
using Ticket.Base.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.DataAccess.Security
{
    public class GroupUserDTO : IGroupUserDTO
    {
        public IEnumerable<IGroupUser> GroupUsers { get; set; }
    }
}

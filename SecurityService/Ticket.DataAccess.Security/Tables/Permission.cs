using Ticket.Base.Entities;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.DataAccess.Security
{
    public partial class Permission : IPermission
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Action { get; set; }

        public string Description { get; set; }

        [Alias("created_by")]
        public long? CreatedBy { get; set; }

        [Alias("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Alias("modified_by")]
        public long? ModifiedBy { get; set; }

        [Alias("modified_date")]
        public DateTime? ModifiedDate { get; set; }

    }
}

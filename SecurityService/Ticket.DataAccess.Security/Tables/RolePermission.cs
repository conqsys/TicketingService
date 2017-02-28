using Ticket.Base;
using Ticket.Base.Entities;
using Ticket.DataAccess.Common;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.DataAccess.Security
{
    [Alias("Role_Permission")]
    [TableWithSequence("Role_Permission", SequenceName = "role_permission_id_seq")]
    public partial class RolePermission : IRolePermission
    {
        [AutoIncrement]
        public long Id { get; set; }

        [Alias("role_id")]
        public long? RoleID { get; set; }

        [Alias("permission_id")]
        public long? PermissionID { get; set; }

        [Alias("created_by")]
        public long? CreatedBy { get; set; }

        [Alias("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Alias("modified_by")]
        public long? ModifiedBy { get; set; }

        [Alias("modified_date")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Permission Action
        /// </summary>
        [Ignore]
        public string Action { get; set; }
        
        /// <summary>
        /// Permission Description
        /// </summary>
        [Ignore]
        public string Description { get; set; }

    }
}

using Ticket.Base;
using Ticket.Base.Entities;
using Newtonsoft.Json;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.DataAccess.TicketService
{
    [TableWithSequence("group_user", SequenceName = "group_user_id_seq")]
    [Alias("group_user")]
    public partial class GroupUser : IGroupUser
    {
        public GroupUser()
        {

        }

        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        [Alias("user_id")]
        [System.ComponentModel.DataAnnotations.Range(1, long.MaxValue)]
        public long UserId { get; set; }

        [Alias("group_id")]
        [System.ComponentModel.DataAnnotations.Range(1, long.MaxValue)]
        public long GroupId { get; set; }


    }
}

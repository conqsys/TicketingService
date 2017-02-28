using Ticket.Base;
using Ticket.Base.Entities;
using Ticket.DataAccess.Common;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.DataAccess.Security
{
    [Alias("User_Password_Log")]
    [TableWithSequence("User_Password_Log", SequenceName = "user_password_log_id_seq")]
    public class UserPasswordLog : IUserPasswordLog
    {
        [AutoIncrement]
        public long Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Alias("User_Id")]
        public long? UserId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Alias("Previous_Password")]
        public string PreviousPassword { get; set; }

        [Alias("Date_Changed")]
        public DateTime? DateChanged { get; set; }
    }
}

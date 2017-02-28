using Ticket.Base;
using Ticket.Base.Entities;
using Ticket.DataAccess.Common;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;

namespace Ticket.DataAccess.Security
{
    [Alias("user_activity_log")]
    [TableWithSequence("user_activity_log", SequenceName ="user_activity_log_id_seq")]
    public partial class UserActivityLog : IUserActivityLog
    {
        public UserActivityLog()
        {
            
        }

        [AutoIncrement]
        public long Id { get; set; }

        [Alias("time_logged")]
        public DateTime? TimeLogged { get; set; }

        [Alias("case_no")]
        public string CaseNo { get; set; }

        [Alias("lab_id")]
        public long? LabId { get; set; }

        [Alias("user_id")]
        public long? UserId { get; set; }

        [Alias("modified_user_id")]
        public long? ModifiedUserId { get; set; }
    }
}

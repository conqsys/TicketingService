using Ticket.Base;
using Ticket.Base.Entities;
using Ticket.DataAccess.Common;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.DataAccess.Security
{
    [Alias("User_Authentication")]
    [TableWithSequence("User_Authentication", SequenceName = "user_authentication_log_id_seq")]
    public class UserAuthentication : IUserAuthentication
    {
        [AutoIncrement]
        public long Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "UserID")]
        [Alias("user_id")]
        public long? UserId { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "IP Address")]
        [Alias("ip_address")]
        public string IpAddress { get; set; }

        [Alias("Verification_Code")]
        public string VerificationCode { get; set; }

        [Alias("Code_Expiration")]
        public DateTime? CodeExpiration { get; set; }


        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "Email")]
        [Alias("Email_Address")]
        public string EmailAddress { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "Phone Number")]
        [Alias("Phone_Number")]
        public string PhoneNumber { get; set; }


        [Alias("created_by")]
        public long? CreatedBy { get; set; }

        [Alias("created_date")]
        public DateTime? CreatedDate { get; set; }

        public bool Verified { get; set; }
    }
}

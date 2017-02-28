using Ticket.Base;
using Ticket.Base.Entities;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.DataAccess.Security
{
    [Alias("lab_contact")]
    [TableWithSequence("lab_contact", SequenceName = "lab_contact_id_seq")]
    public partial class LabContact : ILabContact
    {
        [AutoIncrement]
        public long Id { get; set; }
        
        public long Version { get; set; }

        [Alias("contact_person_email")]
        public string ContactPersonEmail { get; set; }

        [Alias("contact_person_fax")]
        public string ContactPersonFax { get; set; }

        [Alias("contact_person_phone")]
        public string ContactPersonPhone { get; set; }

        [Alias("lab_id")]
        public long LabId { get; set; }

        [Alias("created_by")]
        public long? CreatedBy { get; set; }

        [Alias("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Alias("modified_by")]
        public long? ModifiedBy { get; set; }

        [Alias("modified_date")]
        public DateTime? ModifiedDate { get; set; }

        [Alias("contact_name")]
        public string ContactName { get; set; }

    }
}

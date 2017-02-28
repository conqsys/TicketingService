using Ticket.Base;
using Ticket.Base.Entities;
using Ticket.DataAccess.Common;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.DataAccess.Security
{
    [TableWithSequence("lab", SequenceName = "lab_id_seq")]
    public partial class Lab : ILab
    {
        public Lab()
        {
            InitialLabAdmin = new User();
            LabContact = new Security.LabContact();
        }

        [AutoIncrement]
        public long Id { get; set; }

        public long Version { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Emailpin { get; set; }

        [Alias("lab_desc")]
        public string LabDesc { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "Lab Name")]
        [Alias("lab_name")]
        public string LabName { get; set; }
        
        public string State { get; set; }

        public string Zip { get; set; }

        [Alias("client_portal")]
        public bool ClientPortal { get; set; }

        public bool Locked { get; set; }

        [Alias("no_of_users")]
        public int NoOfUsers { get; set; }

        public bool WebServices { get; set; }

        [Alias("no_of_clients")]
        public int NoOfClients { get; set; }

        [Alias("email_report")]
        public bool EmailReport { get; set; }

        [Alias("fax_report")]
        public bool FaxReport { get; set; }

        [Alias("is_paid")]
        public bool IsPaid { get; set; }

        [Alias("area_code")]
        public string AreaCode { get; set; }

        [Alias("fax_numberfor_report")]
        public string FaxNumberForReport { get; set; }

        [Alias("is_deleted")]
        public bool IsDeleted { get; set; }

        [Alias("specimen_label_type")]
        public string SpecimenLabelType { get; set; }

        [Alias("created_by")]
        public long? CreatedBy { get; set; }

        [Alias("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Alias("Modified_by")]
        public long? ModifiedBy { get; set; }

        [Alias("Modified_date")]
        public DateTime? ModifiedDate { get; set; }

        [Alias("asset_id")]
        public long? AssetId { get; set; }

        [Ignore]
        public string FileData { get; set; }

        [Ignore]
        public string FileName { get; set; }

        [Ignore]
        public IUser InitialLabAdmin { get; set; }

        [Ignore]
        public ILabContact LabContact { get; set; }
    }
}

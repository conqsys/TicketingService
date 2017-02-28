﻿using Ticket.Base;
using Ticket.Base.Entities;
using Newtonsoft.Json;
using SimpleStack.Orm.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ticket.DataAccess.TicketService
{
    [TableWithSequence("request_type", SequenceName = "request_type_id_seq")]
    [Alias("request_type")]
    public partial class RequestType : IRequestType
    {        
        public RequestType()
        {
           
        }

        [PrimaryKey]
        [AutoIncrement]
        public long Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public bool Enabled { get; set; }


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

using Ticket.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.DataAccess.TicketService
{
    public enum EnumErrorCode : Int32
    {

    }
    
    public class ValidationErrorCodes : BaseValidationErrorCodes
    {
        public ValidationErrorCodes()
        {
            this.InitializeErrorCodes();
        }

        public void AddErrorCode(EnumErrorCode errorCode, string message)
        {
            base.AddErrorCode((int)errorCode, message);
        }

        protected override void InitializeErrorCodes()
        {
            base.InitializeErrorCodes();
        }

        public KeyValuePair<int, string> this[EnumErrorCode errorCode, params object[] formatter]
        {
            get
            {
                return base[(int)errorCode, formatter];
            }
        }
    }
}

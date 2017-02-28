using Ticket.Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.DataAccess.Common
{
    public class ValidationError
    {
        public ValidationError(string error)
        {
            this.Error = error;
        }
        public string Error { get; set; }
    }
    //public class APValidationResult : ValidationResult, IValidationResult
    //{
    //    public int ErrorCode { get; set; }
       
    //    public virtual  new string ErrorMessage { get; set; }
       
    //    public virtual  new IEnumerable<string> MemberNames { get; set; }


    //    public APValidationResult(KeyValuePair<int, string> error, string attachedMessage = "") : base(error.Value+" "+ attachedMessage)
    //    {
    //        this.ErrorCode = error.Key;
    //    }

    //    public APValidationResult(string errorMessage, int errorCode) : base(errorMessage)
    //    {
    //        this.ErrorCode = errorCode;
    //    }

    //    public APValidationResult(string errorMessage):this(errorMessage,0)
    //    {

    //    }

    //    public APValidationResult(string errorMessage, IEnumerable<string> memberNames, int errorCode) : base(errorMessage, memberNames)
    //    {
    //        this.ErrorCode = errorCode;
    //    }
    //    public APValidationResult(string errorMessage, IEnumerable<string> memberNames):this(errorMessage,memberNames,0)
    //    {

    //    }

    //    public APValidationResult(ValidationCodeResult ValidationCodeResult) : base(ValidationCodeResult)
    //    {

    //    }
    //}


    public class ValidationCodeResult:ValidationResult,IValidationResult
    {

        public int ErrorCode { get; set; }

        public ValidationCodeResult(KeyValuePair<int,string> error,string attachedMessage="") : base(error.Value+" "+ attachedMessage)
        {
            this.ErrorCode = error.Key;            
        }

        public ValidationCodeResult(string errorMessage,int errorCode) : base(errorMessage)
        {
            this.ErrorCode = errorCode;
        }

        public ValidationCodeResult(string errorMessage):this(errorMessage,0)
        {

        }

        public ValidationCodeResult(string errorMessage, IEnumerable<string> memberNames, int errorCode) : base(errorMessage, memberNames)
        {
            this.ErrorCode = errorCode;
        }
        public ValidationCodeResult(string errorMessage, IEnumerable<string> memberNames):this(errorMessage,memberNames,0)
        {

        }
      
        public ValidationCodeResult(ValidationCodeResult ValidationCodeResult) : base(ValidationCodeResult)
        {

        }

        
    }
}

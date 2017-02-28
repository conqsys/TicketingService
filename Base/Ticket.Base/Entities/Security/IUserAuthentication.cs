
using System;


namespace Ticket.Base.Entities
{
    public interface IUserAuthentication : IEntity, ICreatedStamp
    {
        long? UserId { get; set; }

        string IpAddress { get; set; }

        string VerificationCode { get; set; }

        DateTime? CodeExpiration { get; set; }

        string EmailAddress { get; set; }

        string PhoneNumber { get; set; }

        bool Verified { get; set; }
    }
}

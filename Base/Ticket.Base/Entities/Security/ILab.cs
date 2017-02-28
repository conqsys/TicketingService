using System;

namespace Ticket.Base.Entities
{
    public interface ILab : IVersioned, IStamp
    {
        string Address1 { get; set; }

        string Address2 { get; set; }

        string City { get; set; }

        string Country { get; set; }

        string Emailpin { get; set; }

        string LabDesc { get; set; }

        string LabName { get; set; }

        string State { get; set; }

        string Zip { get; set; }

        bool ClientPortal { get; set; }

        bool Locked { get; set; }

        int NoOfUsers { get; set; }

        bool WebServices { get; set; }

        int NoOfClients { get; set; }

        bool EmailReport { get; set; }

        bool FaxReport { get; set; }

        bool IsPaid { get; set; }

        string AreaCode { get; set; }

        string FaxNumberForReport { get; set; }

        bool IsDeleted { get; set; }

        string SpecimenLabelType { get; set; }

        long? AssetId { get; set; }

        string FileData { get; set; }

        string FileName { get; set; }

        IUser InitialLabAdmin { get; set; }

        ILabContact LabContact { get; set; }
    }
}

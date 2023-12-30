using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using API.Interface;
using API.Utilities.Enum;

namespace API.Entities;

public partial class Vendor : ISoftDeletable
{
    public string Guid { get; set; } = null!;

    public string CompanyGuid { get; set; } = null!;

    [Column("status", TypeName = "enum('WaitingForApproval','Approval','Rejected')")]
    public VendorStatus Status { get; set; }

    public string? ConfirmBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual User? ConfirmByNavigation { get; set; }
}

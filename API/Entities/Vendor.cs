using System;
using System.Collections.Generic;
using API.Interface;

namespace API.Entities;

public partial class Vendor : ISoftDeletable
{
    public string Guid { get; set; } = null!;

    public string CompanyGuid { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? ConfirmBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual User? ConfirmByNavigation { get; set; }
}

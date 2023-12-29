using System;
using System.Collections.Generic;
using API.Interface;

namespace API.Entities;

public partial class ProjectVendor : ISoftDeletable
{
    public string ProjectGuid { get; set; } = null!;

    public string VendorGuid { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}

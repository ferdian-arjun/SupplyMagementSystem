using System;
using System.Collections.Generic;
using API.Interface;

namespace API.Entities;

public partial class Company : ISoftDeletable
{
    public string Guid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telp { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string? BusinessType { get; set; }

    public string? Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Vendor> TblTrVendors { get; set; } = new List<Vendor>();
}

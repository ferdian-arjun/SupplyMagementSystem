using System;
using System.Collections.Generic;
using API.Interface;

namespace API.Entities;

public partial class User : ISoftDeletable
{
    public string Guid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<UserRole> TblTrUserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<Vendor> TblTrVendors { get; set; } = new List<Vendor>();
}

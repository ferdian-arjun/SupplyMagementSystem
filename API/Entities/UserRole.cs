using System;
using System.Collections.Generic;
using API.Interface;

namespace API.Entities;

public partial class UserRole : ISoftDeletable
{
    public string UserGuid { get; set; } = null!;

    public string RoleGuid { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

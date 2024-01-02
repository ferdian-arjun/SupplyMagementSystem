using API.Interface;

namespace API.Entities;

public partial class Role : ISoftDeletable
{
    public string Guid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<UserRole> TblTrUserRoles { get; set; } = new List<UserRole>();
    
    
}

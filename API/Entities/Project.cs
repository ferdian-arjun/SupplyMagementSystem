using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using API.Interface;
using API.Utilities.Enum;

namespace API.Entities;

public partial class Project : ISoftDeletable
{
    public string Guid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
    

    [Column("status", TypeName = "enum('OnPlan','OnProgress','Done','Canceled')")]
    public ProjectStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
    
    public virtual ICollection<ProjectVendor> TblTrProjectVendors { get; set; } = new List<ProjectVendor>();
}

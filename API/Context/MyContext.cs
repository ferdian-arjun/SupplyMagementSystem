using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public partial class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
    }

    public virtual DbSet<Company> TblMCompanies { get; set; }

    public virtual DbSet<Project> TblMProjects { get; set; }

    public virtual DbSet<Role> TblMRoles { get; set; }

    public virtual DbSet<User> TblMUsers { get; set; }

    public virtual DbSet<ProjectVendor> TblTrProjectVendors { get; set; }

    public virtual DbSet<UserRole> TblTrUserRoles { get; set; }

    public virtual DbSet<Vendor> TblTrVendors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("tbl_m_companies");

            entity.Property(e => e.Guid)
                .HasMaxLength(36)
                .HasColumnName("guid");
            entity.Property(e => e.BusinessType)
                .HasMaxLength(100)
                .HasColumnName("business_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(36)
                .HasColumnName("email");
            entity.Property(e => e.Image)
                .HasMaxLength(64)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(36)
                .HasColumnName("name");
            entity.Property(e => e.Telp)
                .HasMaxLength(24)
                .HasColumnName("telp");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("tbl_m_projects");

            entity.Property(e => e.Guid)
                .HasMaxLength(36)
                .HasColumnName("guid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Name)
                .HasMaxLength(225)
                .HasColumnName("name");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasColumnType("enum('OnPlan','OnProgress','Done','Canceled')")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("tbl_m_roles");

            entity.Property(e => e.Guid)
                .HasMaxLength(36)
                .HasColumnName("guid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name)
                .HasMaxLength(36)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("tbl_m_users");

            entity.Property(e => e.Guid)
                .HasMaxLength(36)
                .HasColumnName("guid");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(36)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(64)
                .HasColumnName("full_name");
            entity.Property(e => e.Password)
                .HasMaxLength(36)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(36)
                .HasColumnName("username");
        });

        modelBuilder.Entity<ProjectVendor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_tr_project_vendors");

            entity.HasIndex(e => e.ProjectGuid, "project_guid");

            entity.HasIndex(e => e.VendorGuid, "vendor_guid");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.ProjectGuid)
                .HasMaxLength(36)
                .HasColumnName("project_guid");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.VendorGuid)
                .HasMaxLength(36)
                .HasColumnName("vendor_guid");

            entity.HasOne(d => d.Project).WithMany()
                .HasForeignKey(d => d.ProjectGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_tr_project_vendors_ibfk_1");

            entity.HasOne(d => d.Vendor).WithMany()
                .HasForeignKey(d => d.VendorGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_tr_project_vendors_ibfk_2");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_tr_user_roles");

            entity.HasIndex(e => e.RoleGuid, "role_guid");

            entity.HasIndex(e => e.UserGuid, "user_guid");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.RoleGuid)
                .HasMaxLength(36)
                .HasColumnName("role_guid");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserGuid)
                .HasMaxLength(36)
                .HasColumnName("user_guid");

            entity.HasOne(d => d.Role).WithMany()
                .HasForeignKey(d => d.RoleGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_tr_user_roles_ibfk_2");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_tr_user_roles_ibfk_1");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Guid).HasName("PRIMARY");

            entity.ToTable("tbl_tr_vendors");

            entity.HasIndex(e => e.ConfirmBy, "confirm_by");

            entity.HasIndex(e => e.CompanyGuid, "tbl_tr_vendor_ibfk_1");

            entity.Property(e => e.Guid)
                .HasMaxLength(36)
                .HasColumnName("guid");
            entity.Property(e => e.CompanyGuid)
                .HasMaxLength(36)
                .HasColumnName("company_guid");
            entity.Property(e => e.ConfirmBy)
                .HasMaxLength(36)
                .HasColumnName("confirm_by");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Status)
                .HasColumnType("enum('WaitingForApproval','Approval','Rejected','')")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Company).WithMany(p => p.TblTrVendors)
                .HasForeignKey(d => d.CompanyGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_tr_vendors_ibfk_1");

            entity.HasOne(d => d.ConfirmByNavigation).WithMany(p => p.TblTrVendors)
                .HasForeignKey(d => d.ConfirmBy)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tbl_tr_vendors_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

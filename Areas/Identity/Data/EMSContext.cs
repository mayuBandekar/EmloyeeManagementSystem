using EmloyeeManagementSystem.Areas.Identity.Data;
using EmloyeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmloyeeManagementSystem.Data;

public class EMSContext : IdentityDbContext<EMSUser>
{
    public EMSContext(DbContextOptions<EMSContext> options)
        : base(options)
    {
    }

    public DbSet<RoleMaster> Roles { get; set; }
    public DbSet<UserMaster> Users { get; set; }

    public DbSet<Timesheet> TimesheetMaster { get; set; }
    public DbSet<Breaks> BreakMaster { get; set; }
    public DbSet<Tasks> TaskMaster { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

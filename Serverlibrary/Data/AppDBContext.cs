

using Base.Entites;
using Microsoft.EntityFrameworkCore;

namespace Serverlibrary.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<Emploee> Emploees { get; set; }
        public DbSet<Departrment> Departrment { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<GeneralDepartment> GeneralDepartment { get; set; }
        public DbSet<Twon> Twons { get; set; }
        public DbSet<SystemRoles> SystemRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefrasgTokenInfo> RefrasgTokenInfo { get; set; }
        public DbSet<VacationType> VacationTypes { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<SanctionType> SanctionTypes { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }
        public DbSet<OverTimeType> OverTimeTypes { get; set; }
        public DbSet<OverTime> OverTimes { get; set; }
        public DbSet<Docter> Docters { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}

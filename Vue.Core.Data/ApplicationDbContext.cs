using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Vue.Core.Data.Entities;

namespace Vue.Core.Data
{
    public sealed class ApplicationDbContext : DbContext, IDisposable
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                BeforeSaveChangeUpdateDate();
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Exception raise = ex;
                return Task.FromResult(-1);
            }
        }

        public override int SaveChanges()
        {
            try
            {
                BeforeSaveChangeUpdateDate();
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                Exception raise = ex;
                return -1;
            }
        }

        private void BeforeSaveChangeUpdateDate()
        {
            var traceable = this.ChangeTracker.Entries();
            if (traceable == null) return;            
            // added
            var entityEntries = traceable as EntityEntry[] ?? traceable.ToArray();
            foreach (var item in entityEntries.Where(t => t.State == EntityState.Added))
            {
                if (item.CurrentValues.Properties.Any(x => x.Name == "Gid"))
                    item.Property("Gid").CurrentValue = Guid.NewGuid();
                if (item.CurrentValues.Properties.Any(x => x.Name == "CreatedAt"))
                    item.Property("CreatedAt").CurrentValue = System.DateTime.Now;
                if (item.CurrentValues.Properties.Any(x => x.Name == "UpdatedAt"))
                    item.Property("UpdatedAt").CurrentValue = System.DateTime.Now;
            }

            // modified
            foreach (var item in entityEntries.Where(t => t.State == EntityState.Modified))
            {
                if (item.CurrentValues.Properties.Any(x => x.Name == "UpdatedAt"))
                    item.Property("UpdatedAt").CurrentValue = System.DateTime.Now;
            }
        }

        #region dbSet

        public DbSet<Users> Users { get; set; }
        public DbSet<UsersRoles> UsersRoles { get; set; }
        public DbSet<UsersToken> UsersToken { get; set; }
        public DbSet<Apps> Apps { get; set; }
        public DbSet<AppsApiCollection> AppsApiCollection { get; set; }
        public DbSet<Clinics> Clinics { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RolesApps> RolesApps { get; set; }
        public DbSet<Scheduler> Scheduler { get; set; }
        

        #endregion
    }
}
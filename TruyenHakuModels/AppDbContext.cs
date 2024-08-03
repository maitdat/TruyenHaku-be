using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TruyenHakuModels.Entities;
using static TruyenHakuCommon.Constants.Constants;

namespace TruyenHakuModels
{
    public class AppDbContext : IdentityDbContext<UserAccount>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }
        public DbSet<UserAccount> ApplicationUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Manga> Manga { get; set; }
        //public DbSet<Author> Author { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            DataSeeding.SeedData(builder);
        }

        public override int SaveChanges()
        {
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();
            // get entries that are being Added or Updated
            var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                // try and convert to an Auditable Entity
                var entity = entry.Entity as TruyenHakuCommon.BaseEntityCommon;
                // call PrepareSave on the entity, telling it the state it is in
                entity?.PrepareSave(httpContextAccessor, entry.State);
            }

            var result = base.SaveChanges();
            return result;
        }
        public async Task<int> SaveChangesAsync()
        {
            var httpContextAccessor = this.GetService<IHttpContextAccessor>();
            // get entries that are being Added or Updated
            var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                // try and convert to an Auditable Entity
                var entity = entry.Entity as TruyenHakuCommon.BaseEntityCommon;
                // call PrepareSave on the entity, telling it the state it is in
                entity?.PrepareSave(httpContextAccessor, entry.State);
            }

            var result = await base.SaveChangesAsync();
            return result;

        }
    }
}

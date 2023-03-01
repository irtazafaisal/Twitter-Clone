using Microsoft.EntityFrameworkCore;

namespace Twitter_V1.Models
{
    public class UserContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=USERS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ProcessSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ProcessSave()
        {
            var currentTime = DateTime.UtcNow;
            foreach(var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is Entity))
            {
                var entity = item.Entity as Entity;
                entity.CreatedDate = currentTime.ToString();
                entity.ModifiedDate = currentTime.ToString();
                
            }

            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is Entity))
            {
                var entity = item.Entity as Entity;
                entity.ModifiedDate = currentTime.ToString();
                item.Property(nameof(entity.CreatedDate)).IsModified = false;
            }

        }

    }
}

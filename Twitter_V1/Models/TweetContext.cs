using Microsoft.EntityFrameworkCore;
namespace Twitter_V1.Models
{
    public class TweetContext: DbContext
    {
        public DbSet<Tweet> Tweets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=USERS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}

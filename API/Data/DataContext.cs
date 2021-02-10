using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<DirectMessage> DirectMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DirectMessage>().HasOne(u => u.Reciever).WithMany(m => m.DirectMessagesRecieved).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DirectMessage>().HasOne(u => u.Sender).WithMany(m => m.DirectMessagesSent).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
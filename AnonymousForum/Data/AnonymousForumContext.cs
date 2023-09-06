using Microsoft.EntityFrameworkCore;
using AnonymousForum.Models;

namespace AnonymousForum.Data
{
    public class AnonymousForumContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Models.Thread> Threads { get; set; }
        public DbSet<Reply> Replies { get; set; }

        public AnonymousForumContext(DbContextOptions<AnonymousForumContext> options)
            : base(options)
        {
            Topics = Set<Topic>();
            Threads = Set<Models.Thread>();
            Replies = Set<Reply>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>()
                .HasIndex(t => t.TopicName)
                .IsUnique();

            modelBuilder.Entity<Models.Thread>()
                .HasIndex(t => t.ThreadTitle)
                .IsUnique();

            modelBuilder.Entity<Topic>()
                .HasMany(t => t.Threads)
                .WithOne(thread => thread.Topic)
                .HasForeignKey(thread => thread.FkTopicId);

            modelBuilder.Entity<AnonymousForum.Models.Thread>()
                .HasMany(thread => thread.Replies)
                .WithOne(reply => reply.Thread)
                .HasForeignKey(reply => reply.FkThreadId);
        }

        public DbSet<AnonymousForum.Models.Account>? Account { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnonymousForum.Models;
using Humanizer.Localisation;

namespace AnonymousForum.Data
{
    public class AnonymousForumContext : DbContext
    {
        public DbSet<AnonymousForum.Models.Topic> Topics { get; set; }
        public DbSet<AnonymousForum.Models.Thread>? Threads { get; set; }
        public DbSet<AnonymousForum.Models.Reply> Replies { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AnonymousForumContext(DbContextOptions<AnonymousForumContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options){ }

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
    }
}

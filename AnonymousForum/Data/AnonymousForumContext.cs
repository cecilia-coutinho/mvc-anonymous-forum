using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnonymousForum.Models;

namespace AnonymousForum.Data
{
    public class AnonymousForumContext : DbContext
    {
        public AnonymousForumContext (DbContextOptions<AnonymousForumContext> options)
            : base(options)
        {
        }

        public DbSet<AnonymousForum.Models.Topic> Topic { get; set; } = default!;

        public DbSet<AnonymousForum.Models.Thread>? Thread { get; set; }

        public DbSet<AnonymousForum.Models.Reply>? Reply { get; set; }
    }
}

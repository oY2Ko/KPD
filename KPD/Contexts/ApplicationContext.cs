using KPD.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPD.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Contestant> Contestants { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

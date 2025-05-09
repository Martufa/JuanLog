using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JuanLog.Models
{
    public class JuanLogDBContext : DbContext
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB; Initial Catalog=JuanLogDB; Integrated Security=true";

        public DbSet<User> Users { get; set; }
        public DbSet<ExerciseEntry> ExerciseEntries { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseCategory> ExerciseCategories { get; set; }
        public DbSet<Set> SetTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
            Debug.WriteLine("Anyway, connection established");
        }

        
    }
}

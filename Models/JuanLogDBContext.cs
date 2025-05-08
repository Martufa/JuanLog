using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JuanLog.Models
{
    public class JuanLogDBContext : DbContext
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB; Initial Catalog=JuanLogDB; Integrated Security=true";

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JuanLog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Permission { get; set; }

        public static string Fucker()
        {
            return "Certified, yassified";
        }

        public async Task<List<User>> GetAllUsers()
        {
            using var db = new JuanLogDBContext();
            return await db.Users.ToListAsync();
        }
        public async Task AddUser()
        {
            using var db = new JuanLogDBContext();
            db.Users.Add(new User { Name = this.Name, Permission = this.Permission });
            await db.SaveChangesAsync();

        }
    }
}

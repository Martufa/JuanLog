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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
            Debug.WriteLine("Anyway, connection established");
        }

        public User? CheckUserPassword(string user, string password)
        {
            Debug.WriteLine("Entered pwd checker");
            using var db = new JuanLogDBContext();

            Debug.WriteLine(user);
            Debug.WriteLine(user == null);
            List<User> matchingUsers = db.Users.Where(u => u.Name == user).ToList();
            
            if (matchingUsers.Count == 0) { Debug.WriteLine("Ended with no user ://"); return null; }

            User activeUser = matchingUsers.First();
            Debug.WriteLine("eee... got the user");

            string actualHashedPassword = activeUser.HashedPassword;
            Debug.WriteLine("Received hashed pwd");

            byte[] hashBytes = Convert.FromBase64String(actualHashedPassword);
            
            // RETRIEVE SALT
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            Debug.WriteLine("Got salt");

            // HASH THE CLAIMED PASSWORD WITH SALT
            var hashMaker = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = hashMaker.GetBytes(16);

            Debug.WriteLine("compare hash made");
            byte[] combinedBytes = new byte[32];
            Array.Copy(salt, 0, combinedBytes, 0, 16);
            Array.Copy(hash, 0, combinedBytes, 16, 16);
            string hashedPassword = Convert.ToBase64String(combinedBytes);

            Debug.WriteLine("Magiccc!");
            // COMPARE HASHES
            if (hashedPassword != actualHashedPassword)
            {
                Debug.WriteLine("Wrong password");
                Debug.WriteLine(hashedPassword);
                Debug.WriteLine(actualHashedPassword);
                return null;
            }

            Debug.WriteLine("Returned from here with user");
            return activeUser;

        }
    }
}

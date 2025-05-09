using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Cryptography;

namespace JuanLog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Permission { get; set; }
        public string HashedPassword {  get; set; }

        public static string Fucker()
        {
            return "Certified, yassified";
        }

        public async Task<List<User>> GetAllUsers()
        {
            using var db = new JuanLogDBContext();
            return await db.Users.ToListAsync();
        }
        public async Task AddUser(string name, int permission, string hashedPassword)
        {
            using var db = new JuanLogDBContext();
            db.Users.Add(new User { Name = name, Permission = permission, HashedPassword = hashedPassword});
            await db.SaveChangesAsync();

        }

        public static User? CheckUserPassword(string user, string password)
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

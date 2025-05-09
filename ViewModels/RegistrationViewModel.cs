using System.Diagnostics;
using System.Security.Cryptography;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    public partial class RegistrationViewModel
    {

        public async void Registrate(string username, string pwd)
        {
            Debug.WriteLine(pwd);
            var db = new JuanLogDBContext();
            // MAKE SALT
            byte[] salt = new byte[16];
            using (var saltMaker = RandomNumberGenerator.Create())
            {
                saltMaker.GetBytes(salt);
            }

            // HASH IT
            var hashMaker = new Rfc2898DeriveBytes(pwd, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = hashMaker.GetBytes(16);

            // COMBINE IT AND STRINGIFY IT
            byte[] combinedBytes = new byte[32];
            Array.Copy(salt, 0, combinedBytes, 0, 16);
            Array.Copy(hash, 0, combinedBytes, 16, 16);
            string hashedPassword = Convert.ToBase64String(combinedBytes);

            Debug.WriteLine("DÉLKA");
            Debug.WriteLine(hashedPassword.Length);

            // SAVE THE USER INTO DB
            User activeUser = new User { Name = username, Permission = 1, HashedPassword = hashedPassword };
            await db.Users.AddAsync(activeUser);
            await db.SaveChangesAsync();

            Debug.WriteLine("Registration complete");
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(activeUser));
        }


        [RelayCommand]
        public void ToLoginCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }
    }
}

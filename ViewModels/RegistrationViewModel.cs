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

        public void Registrate(string username, string pwd)
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
            var hashMaker = new Rfc2898DeriveBytes(pwd, salt, 100000, HashAlgorithmName.SHA3_256);
            byte[] hash = hashMaker.GetBytes(32);

            // COMBINE IT AND STRINGIFY IT
            byte[] combinedBytes = new byte[36];
            Array.Copy(salt, 0, combinedBytes, 0, 16);
            Array.Copy(hash, 0, combinedBytes, 16, 20);
            string hashedPassword = Convert.ToBase64String(combinedBytes);

            // SAVE THE USER INTO DB
            db.Users.AddAsync(new User { Name = username, Permission = 1, HashedPassword = hashedPassword });

            Debug.WriteLine("Registration complete");
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage());
        }


        [RelayCommand]
        public void ToLoginCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }
    }
}

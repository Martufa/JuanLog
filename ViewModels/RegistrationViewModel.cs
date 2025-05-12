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
            string hashedPassword = User.HashPassword(pwd);


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

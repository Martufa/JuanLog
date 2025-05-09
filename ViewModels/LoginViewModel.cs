using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JuanLog.Models;
using System.Windows.Controls;
using System.Security;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;


namespace JuanLog.ViewModels
{
    [ObservableObject]
    partial class LoginViewModel
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private List<User> _displayUser;

        [ObservableProperty]
        private SecureString _securePwd;

        public LoginViewModel()
        {
            User helper = new User();
            _username = User.Fucker();
            _displayUser = new List<User>();
            _securePwd = new SecureString();
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }


        public void LogIn(string username, string pwd)
        {
            Debug.WriteLine(pwd);
            var passwordCheck = new JuanLogDBContext();
            // HashCode(pwd);


            // pokud se shoduje
            // zapiš ho jako aktivního uživatele - ostatní modely ho potřebují (progress, add exercise, import, profile)
            User activeUser = passwordCheck.CheckUserPassword(username, pwd).Result;
            if (activeUser == null)
            {
                Debug.WriteLine("Špatné jméno nebo heslo >.<");
                return;
            } 

            // a skoč na homepage
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage());
        }


        [RelayCommand]
        public void SetUsername()
        {
            Username = "JEZUUUU, SLAYYYYY!";
            Debug.WriteLine("Měli bychom slayovat...");
        }

        [RelayCommand]
        public async void GetJuanLogUsers()
        {
            User helper = new User();
            DisplayUser = await helper.GetAllUsers();
            foreach (User user in DisplayUser)
            {
                Debug.WriteLine(user.Name);
            }
        }

        [RelayCommand]
        public void ToRegistrationCommand()
        {
            Debug.WriteLine("Ok, switch to registration command recieved");
            WeakReferenceMessenger.Default.Send(new ShowRegistrationViewMessage());
        }

        [RelayCommand]
        public void ToLoginCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }


    }
}

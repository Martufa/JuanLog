using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JuanLog.Models;

using System.Security;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using System.Windows;


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
            _username = "Nikdo";
            _displayUser = new List<User>();
            _securePwd = new SecureString();
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }


        public void LogIn(string username, string pwd)
        {
            User? activeUser = User.CheckUserPassword(username, pwd);
            if (activeUser == null)
            {
                MessageBox.Show("Špatné jméno nebo heslo >.<");
                return;
            }

            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(activeUser));
        }

        [RelayCommand]
        public void ToRegistrationCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowRegistrationViewMessage());
        }

        [RelayCommand]
        public void ToLoginCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }


    }
}

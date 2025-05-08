using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

using JuanLog.ViewModels;

namespace JuanLog.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void LogInToVM(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Sending login data from View to VM");
            var vm = (LoginViewModel)this.DataContext;
            vm.LogIn(UsernameBox.Text, PasswordBox.Password);
        }
    }
}

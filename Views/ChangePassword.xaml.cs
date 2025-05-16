using System.Diagnostics;
using System.Windows;
using JuanLog.ViewModels;


namespace JuanLog.Views
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        public void AbortPasswordChange(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public async void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            if (! (NewPasswordControlBox.Password == NewPasswordBox.Password))
            {
                MessageBox.Show("Zadaná hesla se neshodují");
                return;
            }
            var vm = (ProfileViewModel)this.DataContext;
            await vm.SaveNewPassword(NewPasswordBox.Password);
            this.Close();
        }
    }
}

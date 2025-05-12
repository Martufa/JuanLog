using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JuanLog.ViewModels;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
            DataContext = new ProfileViewModel();
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Sending registration data from View to VM");
            var vm = (ProfileViewModel)this.DataContext;
            vm.SaveNewPassword(NewPasswordBox.Text);
        }
    }
}

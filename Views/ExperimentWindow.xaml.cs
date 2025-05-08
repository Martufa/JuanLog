using System.Windows;
using System.Windows.Controls;
using JuanLog.ViewModels;

namespace JuanLog.Views
{
    /// <summary>
    /// Interaction logic for ExperimentWindow.xaml
    /// </summary>
    public partial class ExperimentWindow : Window
    {
        public ExperimentWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
    }
}

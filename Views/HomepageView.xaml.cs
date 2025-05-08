using System.Windows.Controls;
using JuanLog.ViewModels;

namespace JuanLog.Views
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class HomepageView : UserControl
    {
        public HomepageView()
        {
            InitializeComponent();
            DataContext = new HomepageViewModel();
        }
    }
}

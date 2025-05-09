using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    public partial class ImportDataViewModel
    {
        public User activeUser { get; set; }
        public ImportDataViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowHomepageMessage>(this, (r, m) =>
            {
                activeUser = m.Value;
            });
            activeUser = new User();
        }

        [RelayCommand]
        public void ToHomepageCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(activeUser));
        }
    }
}

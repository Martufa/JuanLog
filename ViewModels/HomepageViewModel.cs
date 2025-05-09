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
    public partial class HomepageViewModel
    {
        public User activeUser { get; set; }
        public HomepageViewModel() {
            WeakReferenceMessenger.Default.Register<ShowHomepageMessage>(this, (r, m) =>
            {
                activeUser = m.Value as User;
            });
            activeUser = new User();
        }
        

        [RelayCommand]
        public void ToImport()
        {
            WeakReferenceMessenger.Default.Send(new ShowImportMessage(activeUser));
        }

        [RelayCommand]
        public void ToAddExercise()
        {
            WeakReferenceMessenger.Default.Send(new ShowAddExerciseMessage(activeUser));
        }

        [RelayCommand]
        public void ToLogin()
        {
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }

        [RelayCommand]
        public void ToProfile()
        {
            WeakReferenceMessenger.Default.Send(new ShowProfileMessage(activeUser));
        }

        [RelayCommand]
        public void ToProgress()
        {
            WeakReferenceMessenger.Default.Send(new ShowProgressMessage(activeUser));
        }
    }
}

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
        public User loggedInUser { get; }

        [RelayCommand]
        public void ToImport()
        {
            WeakReferenceMessenger.Default.Send(new ShowImportMessage());
        }

        [RelayCommand]
        public void ToAddExercise()
        {
            WeakReferenceMessenger.Default.Send(new ShowAddExerciseMessage());
        }

        [RelayCommand]
        public void ToLogin()
        {
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }

        [RelayCommand]
        public void ToProfile()
        {
            WeakReferenceMessenger.Default.Send(new ShowProfileMessage());
        }

        [RelayCommand]
        public void ToProgress()
        {
            WeakReferenceMessenger.Default.Send(new ShowProgressMessage());
        }
    }
}

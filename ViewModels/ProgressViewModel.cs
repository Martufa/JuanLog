using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    partial class ProgressViewModel
    {
        [ObservableProperty]
        private User _activeUser;

        [ObservableProperty]
        private List<ExerciseEntry> _exerciseEntries;
        public ProgressViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowProgressMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value;
                updateEntries();
            });
            _activeUser = new User();
            _exerciseEntries = new List<ExerciseEntry>();
            updateEntries();
        }

        private async void updateEntries()
        {
            ExerciseEntries = await ExerciseEntry.GetAllUserEntries(ActiveUser);
        }

        [RelayCommand]
        public void ShowEntriesButton()
        {
            MessageBox.Show(ActiveUser.Id.ToString());
        }

        [RelayCommand]
        public void ToHomepageCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(ActiveUser));
        }
    }
}

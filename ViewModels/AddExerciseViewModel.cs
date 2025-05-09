using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;
using JuanLog.ViewModels;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    public partial class AddExerciseViewModel
    {
        [ObservableProperty]
        private User _activeUser;
        public AddExerciseViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowHomepageMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value;
            });
            _activeUser = new User();
        }
        [RelayCommand]
        public void ToHomepage()
        {
            Debug.WriteLine("I want to go home!");
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(ActiveUser));
        }
    }
}
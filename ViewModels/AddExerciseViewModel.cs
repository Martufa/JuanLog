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

        [ObservableProperty]
        private ExerciseEntry _entry;
        public AddExerciseViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowHomepageMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value;
            });
            _activeUser = new User();
            _entry = new ExerciseEntry();
        }
        [RelayCommand]
        public void ToHomepage()
        {
            Debug.WriteLine("I want to go home!");
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(ActiveUser));
        }

        [RelayCommand]
        public async Task AddExerciseEntry()
        {
            var db = new JuanLogDBContext();
            // db.ExerciseEntries.Add();
            await db.SaveChangesAsync();
        }
    }
}
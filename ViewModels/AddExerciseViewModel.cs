using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media.Media3D;
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

        [ObservableProperty]
        private List<Exercise> _exercises;

        [ObservableProperty]
        private ObservableCollection<int> _repetitions;

        [ObservableProperty]
        private int _weight;

        [ObservableProperty]
        private int _currentSet;

        [ObservableProperty]
        private Exercise _selectedExercise;
        public AddExerciseViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowHomepageMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value;
            });
            _activeUser = new User();
            _entry = new ExerciseEntry();
            _exercises = new List<Exercise>();
            _repetitions = new ObservableCollection<int>();
            _weight = 0;
            Repetitions.Add(3);
            _currentSet = 0;

            updateExercises();
            Debug.WriteLine("Aaaa,a");
        }

        private async void updateExercises()
        {
            Exercises = await Exercise.GetAllExercises();
        }

        [RelayCommand]
        public void ToHomepage()
        {
            Debug.WriteLine("I want to go home!");
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(ActiveUser));
        }

        [RelayCommand]
        public async Task AddExerciseEntry() // Exercise selectedExercise
        {
            
            var db = new JuanLogDBContext();
            // zapiš entry
            var addedEntry = db.ExerciseEntries.Add(new ExerciseEntry { UserId = ActiveUser.Id, ExerciseName = SelectedExercise.ExerciseName, Weight = Weight, When = DateTime.Now });
            await db.SaveChangesAsync();


            // získej zpátky jeho id
            var entryId = addedEntry.Property(x => x.EntryId).CurrentValue;

            // vytvoř a zapiš do SetTables všechny repy, které Juan udělal, přiřaď jim jako EntryId to id, co jsi právě vydoloval
            foreach (int setRepetition in Repetitions)
            {
                db.SetTable.Add(new Set { EntryId = entryId, Repetitions = setRepetition });
                await db.SaveChangesAsync();
            }
            MessageBox.Show("Úspěšně přidáno do databáze! ^^");
        }

        [RelayCommand]
        public void AddSet()
        {
            Repetitions.Add(CurrentSet);
        }


        [RelayCommand]
        public void RemoveLastSet()
        {
            if (Repetitions.Count > 0)
            {
                Repetitions.RemoveAt(Repetitions.Count - 1);
            }
        }
    }
}
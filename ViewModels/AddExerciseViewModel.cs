using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
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
        private int _weight;

        [ObservableProperty]
        private ObservableCollection<int> _repetitions;

        [ObservableProperty]
        private int _currentSet;

        [ObservableProperty]
        private Exercise _selectedExercise;

        [ObservableProperty]
        private List<ExerciseCategory> _categories;

        [ObservableProperty]
        private ExerciseCategory _selectedCategory;
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
            _currentSet = 0;
            _categories = new List<ExerciseCategory>();

            UpdateExercises();
            UpdateCategories();
        }

        [RelayCommand]
        public void ToHomepage()
        {
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(ActiveUser));
        }

        [RelayCommand]
        public async Task AddExerciseEntry()
        {
            ExerciseEntry addedEntry = await ExerciseEntry.AddEntry(new ExerciseEntry {
                UserId = ActiveUser.Id,
                ExerciseCategory = 1,
                ExerciseName = SelectedExercise.ExerciseName,
                Weight = Weight,
                When = DateTime.Now,
                Sets = Repetitions.Count
            });

            Entry.EntryId = addedEntry.EntryId;
            UpdateSets();
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
        public ExerciseEntry GetExerciseEntry()
        {
            return new ExerciseEntry { EntryId=Entry.EntryId, 
                UserId = ActiveUser.Id, 
                ExerciseName = SelectedExercise.ExerciseName, 
                Weight = Weight, 
                When = Entry.When, 
                Sets = Repetitions.Count,
                ExerciseCategory = SelectedCategory.CategoryId,
            };
        }

        private async void UpdateExercises()
        {
            Exercises = await Exercise.GetAllExercises();
        }

        private async void UpdateCategories()
        {
            Categories = await ExerciseCategory.GetAllCategories();
        }

        public async void UpdateShownSets()
        {
            List<Set> allSets = await ExerciseEntry.GetEntrySets(Entry);
            ObservableCollection<int> newRepSet = new();
            foreach (Set set in allSets)
            {
                newRepSet.Add(set.Repetitions);
            }
            Repetitions = newRepSet;
        }

        public async void UpdateSets()
        {
            await ExerciseEntry.UpdateEntrySets(Entry.EntryId, Repetitions);
        }

        public async void UpdateSelectedExerciseFromExName(string exerciseName)
        {
            SelectedExercise = await Exercise.GetExerciseByName(exerciseName) ?? SelectedExercise;
        }

        
    }
}
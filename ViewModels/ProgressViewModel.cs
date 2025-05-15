using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;
using LiveCharts.Wpf;
using LiveCharts;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    partial class ProgressViewModel
    {
        [ObservableProperty]
        private User _activeUser;

        [ObservableProperty]
        private List<ExerciseEntry> _exerciseEntries;

        [ObservableProperty]
        private List<Exercise> _exercises;

        [ObservableProperty]
        private ObservableCollection<ExerciseEntry> _selectedEntries;

        [ObservableProperty]
        private SeriesCollection _addedWeight;
        public ProgressViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowProgressMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value;
                updateEntries();
            });
            _activeUser = new User();
            _exerciseEntries = new List<ExerciseEntry>();
            _exercises = new List<Exercise>();
            updateEntries();
            updateExercises();
        }

        private async void updateEntries()
        {
            ExerciseEntries = await ExerciseEntry.GetAllUserEntries(ActiveUser);
            updateWeightGraph();
        }

        private void updateWeightGraph()
        {
            LineSeries graphValues = new LineSeries();
            graphValues.Values = new ChartValues<int> { };
            foreach (int w in ExerciseEntries.Select(e => e.Weight))
            {
                graphValues.Values.Add(w);
            }

            AddedWeight = new() { graphValues };
        }

        private async void updateExercises()
        {
            Exercises = await Exercise.GetAllExercises();
        }


        [RelayCommand]
        public async Task Filter(string filterExerciseName)
        {
            ExerciseEntries = await ExerciseEntry.GetAllUserEntries(ActiveUser);
            if (filterExerciseName != "")
            { 
                ExerciseEntries = ExerciseEntries.Where(e => e.ExerciseName == filterExerciseName).ToList();
            }
            updateWeightGraph();
        }


        [RelayCommand]
        public void UpdateEntryTable()
        {

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

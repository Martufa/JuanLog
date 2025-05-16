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
using LiveCharts.Defaults;
using System.Windows.Data;
using JuanLog.Views;
using Microsoft.EntityFrameworkCore;
using static CommunityToolkit.Mvvm.ComponentModel.__Internals.__TaskExtensions.TaskAwaitableWithoutEndValidation;

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

        [ObservableProperty]
        private List<int> _labels;

        [ObservableProperty]
        private ChartValues<HeatPoint> _streakValues;

        public Func<double, string> YFormatter { get; set; }
        public ProgressViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowProgressMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value;
                UpdateEntries();
                UpdateShownExercises();
            });

            WeakReferenceMessenger.Default.Register<ExerciseEntryChangedMessage>(this, (r, m) =>
            {
                MessageBox.Show("Shit registered!");
                UpdateEntryToDb(m.Value);
                UpdateEntries();

            });

            _activeUser = new User();
            _exerciseEntries = new List<ExerciseEntry>();
            _exercises = new List<Exercise>();
            _labels = new List<int>();
            YFormatter = value => ((int)value).ToString("D");
            _streakValues = new();

            UpdateEntries();
            UpdateShownExercises();
        }

        private async void UpdateEntries()
        {
            ExerciseEntries = await ExerciseEntry.GetAllUserEntries(ActiveUser);
            UpdateWeightGraph();
            MakeHeatMap();
        }

        private List<DateTime> GetDaysSince(DateTime start)
        {
            return Enumerable.Range(0, (DateTime.Now - start).Days + 1)
                     .Select(offset => start.AddDays(offset))
                     .ToList();
        }
        public void MakeHeatMap()
        {
            Dictionary<DateTime, int> entriesInDays = new();
            DateTime earliestDate = DateTime.Now;
            foreach (ExerciseEntry e in ExerciseEntries)
            {
                if (DateTime.Compare(e.When, earliestDate) < 0)
                {
                    earliestDate = e.When;
                }
                if (! entriesInDays.ContainsKey(e.When))
                {
                    entriesInDays.Add(e.When, 0);
                }
                entriesInDays[e.When]++;
            }

            List<DateTime> allDays = GetDaysSince(earliestDate);
            ChartValues<HeatPoint> allHeatPoints = new();
            int row = allDays.Count / 14;
            int col = 0;
            foreach (DateTime d in allDays)
            {
                if (entriesInDays.ContainsKey(d))
                {
                    allHeatPoints.Add(new HeatPoint() { X = col, Y = row, Weight = entriesInDays[d] });
                }
                else
                {
                    allHeatPoints.Add(new HeatPoint() { X = col, Y = row, Weight = 0 });
                }
                col++;
                if (col == 15)
                {
                    col = 0;
                    row--;
                }
            }
            StreakValues = allHeatPoints;
        }

        private void UpdateWeightGraph()
        {
            ColumnSeries graphValues = new();
            graphValues.Values = new ChartValues<int> { };
            foreach (int w in ExerciseEntries.Select(e => e.Weight))
            {
                graphValues.Values.Add(w);
            }
            Labels = Enumerable.Range(1, graphValues.Values.Count + 1).ToList();
            AddedWeight = new() { graphValues };
        }

        private async void UpdateShownExercises()
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
            UpdateWeightGraph();
            MakeHeatMap();
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

        public void ChangeEntry(ExerciseEntry exerciseEntry)
        {
            var popUp = new ChangeEntryPopupWindow(exerciseEntry);
            popUp.Show();
        }

        public async void RemoveFromDB(ExerciseEntry removeEntry)
        {
            await ExerciseEntry.RemoveEntry(removeEntry);
            UpdateEntries();
        }
        public async void UpdateEntryToDb(ExerciseEntry editedEntry)
        {
            await ExerciseEntry.UpdateEntry(editedEntry);
            UpdateEntries();
        }

    }
}

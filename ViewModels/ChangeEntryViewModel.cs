using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using JuanLog.Models;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    public partial class ChangeEntryViewModel
    {
        [ObservableProperty]
        private ExerciseEntry _editedEntry;

        [ObservableProperty]
        private List<Exercise> _exercises;
        public ChangeEntryViewModel(ExerciseEntry exerciseEntry)
        {
            _editedEntry = exerciseEntry;
            _exercises = new List<Exercise>();

            updateExercises();
        }
        private async void updateExercises()
        {
            Exercises = await Exercise.GetAllExercises();
        }
        public ExerciseEntry GetExerciseEntry()
        {
            return EditedEntry;
        }
    }
}

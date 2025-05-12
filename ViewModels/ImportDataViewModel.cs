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
using Microsoft.Win32;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


namespace JuanLog.ViewModels
{
    [ObservableObject]
    public partial class ImportDataViewModel
    {
        [ObservableProperty]
        private User _activeUser;
        public ImportDataViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowImportMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value;
            });
            _activeUser = new User();
        }

        [RelayCommand]
        public void ToHomepageCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(ActiveUser));
        }

        [RelayCommand]
        public void OpenImportFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ProcessCSV(openFileDialog.FileName);
            }

            MessageBox.Show("Import dokončen");
        }

        private async void ProcessCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
            var db = new JuanLogDBContext();
            List<Exercise> allExercises = await Exercise.GetAllExercises();

            foreach (string line in lines)
            {
                string[] data = line.Split(';');
                if (data.Length == 1) // Juan ten den necvičil
                {
                    continue;
                }
                int weight = 0;
                DateTime date = DateTime.Now;
                try
                {
                    weight = int.Parse(data[2]);
                    date = DateTime.Parse(data[0]);
                } catch
                {
                    MessageBox.Show("Něco z toho je špatně ve dni: " + data[0]);
                    // weight vyhodí chybu, juan ten den necvičil
                }
                
                
                for (int i = 0; i < 5; i++)
                {
                    string exerciseName = data[6 * i + 1];
                    if (! allExercises.Select(e => e.ExerciseName).Contains(exerciseName))
                    {
                        var addition = new Exercise { ExerciseName = exerciseName, CategoryId = 1 };
                        db.Exercises.Add(addition);
                        await db.SaveChangesAsync();

                        allExercises.Add(addition);
                    }
                    ExerciseEntry entry = new ExerciseEntry { UserId = ActiveUser.Id, ExerciseName = exerciseName, Weight = weight, When = date };
                    var addedEntry = await db.ExerciseEntries.AddAsync(entry);
                    await db.SaveChangesAsync();

                    var entryId = addedEntry.Property(x => x.EntryId).CurrentValue;
                    for (int rep = 0; rep < 4; rep++)
                    {
                        try
                        {
                            int currentReps = int.Parse(data[6 * i + 1 + rep]);
                            db.SetTable.Add(new Set { EntryId = entryId, Repetitions = currentReps });
                        }
                        catch {
                            MessageBox.Show("Zapsání setu " + date + " se nepodařilo...promiň, Juane.");
                        }
                    }


                }
            }
        }
    }
}

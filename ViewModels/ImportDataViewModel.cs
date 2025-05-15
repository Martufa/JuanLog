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
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


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
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(ActiveUser));
        }

        private async void ProcessCSV(string fileName)
        {
            try { 
                string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
                var db = new JuanLogDBContext();

                ConcurrentDictionary<string, Exercise> allExercises = new();
                foreach (Exercise e in await Exercise.GetAllExercises())
                {
                    allExercises.TryAdd(e.ExerciseName, e);
                }

                foreach (string line in lines)
                {
                    string[] data = line.Split(';');
                    if (data.Length == 1) // Juan did not exercise
                    {
                        continue;
                    }
                    int weight = 0;
                    DateTime date = DateTime.Parse(data[0]);
                    weight = int.Parse(data[2]);
                

                    for (int i = 0; i < 5; i++)
                    {
                        string exerciseName = data[6 * i + 1];
                        if (exerciseName == "")
                        {
                            break;
                        }
                    
                        // if the exercise doesn't exist, add it
                        if (! allExercises.ContainsKey(exerciseName))
                        {
                            var addition = new Exercise { ExerciseName = exerciseName, CategoryId = 1 };
                            try
                            {   
                                allExercises.TryAdd(exerciseName, addition);
                                db.Exercises.Add(addition);

                                await db.SaveChangesAsync();
                            } catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                            {
                                // this exception is thrown if two same keys are being inserted by different threads:
                                // the primary keys cannot allow for duplicities
                                // should this error occur, we can just insert the exercise as-is, without creating new category
                                // we just need to remove it from the log
                                db.Exercises.Entry(addition).State = EntityState.Detached;
                            }
                        }
                    
                        ExerciseEntry entry = new ExerciseEntry { UserId = ActiveUser.Id, ExerciseName = exerciseName, Weight = weight, When = date };
                        var addedEntry = await db.ExerciseEntries.AddAsync(entry);
                        await db.SaveChangesAsync();

                        var entryId = addedEntry.Property(x => x.EntryId).CurrentValue;
                        for (int rep = 1; rep <= 5; rep++)
                        {
                            string repString = data[6 * i + 1 + rep];
                            if (repString == "")
                            {
                                break;
                            }
                            int currentReps = (int)(float.Parse(repString, CultureInfo.InvariantCulture));
                            db.SetTable.Add(new Set { EntryId = entryId, Repetitions = currentReps });

                        }
                        await db.SaveChangesAsync();


                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Načítání z csv souboru selhalo. Zkontroluj prosím formát souboru.");
            }
        }
    }
}

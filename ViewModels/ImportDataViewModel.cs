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
            MessageBox.Show("Yes");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                // string fileName, public IEnumerable<Person> ReadCSV()
                ProcessCSV(openFileDialog.FileName);
            }

            MessageBox.Show("Asi ok 0.o");
        }

        private async void ProcessCSV(string fileName)
        {
            string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
            var db = new JuanLogDBContext();

            foreach (string line in lines)
            {
                string[] data = line.Split(';');
                int weight = int.Parse(data[2]);
                DateTime date = DateTime.Parse(data[0]);

                for (int i = 1; i < 6; i++)
                {
                    ExerciseEntry entry = new ExerciseEntry { UserId = ActiveUser.Id, ExerciseName = data[i], Weight = weight, When = date };
                    var addedEntry = await db.ExerciseEntries.AddAsync(entry);
                    await db.SaveChangesAsync();

                    var entryId = addedEntry.Property(x => x.EntryId).CurrentValue;
                    for (int rep = 0; rep < 4; rep++)
                    {
                        try
                        {
                            int currentReps = int.Parse(data[i + rep]);
                            db.SetTable.Add(new Set { EntryId = entryId, Repetitions = currentReps });
                        }
                        catch { }
                    }


                }
            }
        }
    }
}

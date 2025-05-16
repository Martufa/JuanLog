using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace JuanLog.Models
{
    public partial class ExerciseEntry
    {
        [Key]
        public int EntryId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("Exercises")]
        public string ExerciseName { get; set; }

        [ForeignKey("ExerciseCategories")]
        public int CategoryId { get; set; }
        public DateTime When { get; set; }
        public int Weight { get; set; }

        public int Sets { get; set; }

        public static async Task<List<ExerciseEntry>> GetAllUserEntries(User user)
        {
            var db = new JuanLogDBContext();
            return await db.ExerciseEntries.Where(e => e.UserId == user.Id).ToListAsync();
        }
        public static async Task<ExerciseEntry> AddEntry(ExerciseEntry toAdd)
        {
            var db = new JuanLogDBContext();
            var addedEntry = db.ExerciseEntries.Add(toAdd);
            await db.SaveChangesAsync();
            return addedEntry.Entity;
        }
        public static async Task RemoveEntry(ExerciseEntry removeEntry)
        {
            var db = new JuanLogDBContext();
            try
            {
                List<Set> removeSets = await ExerciseEntry.GetEntrySets(removeEntry);
                foreach (Set set in removeSets)
                {
                    db.SetTable.Remove(set);
                }
                await db.SaveChangesAsync();
                db.ExerciseEntries.Remove(removeEntry);
                await db.SaveChangesAsync();
            }
            catch
            {
                MessageBox.Show("Mazání neproběhlo úspěšně, promiň...");
            }
        }
        public static async Task UpdateEntry(ExerciseEntry editedEntry)
        {
            var db = new JuanLogDBContext();
            try
            {
                (await db.ExerciseEntries.Where(e => e.EntryId == editedEntry.EntryId).FirstAsync()).Weight = editedEntry.Weight;
                (await db.ExerciseEntries.Where(e => e.EntryId == editedEntry.EntryId).FirstAsync()).When = editedEntry.When;
                (await db.ExerciseEntries.Where(e => e.EntryId == editedEntry.EntryId).FirstAsync()).ExerciseName = editedEntry.ExerciseName;
                await db.SaveChangesAsync();
            }
            catch
            {
                MessageBox.Show("Uložení se nezdařilo");
            }
        }
        public static async Task<List<Set>> GetEntrySets(ExerciseEntry exerciseEntry)
        {
            var db = new JuanLogDBContext();
            return await db.SetTable.Where(s => s.EntryId == exerciseEntry.EntryId).ToListAsync();
        }
        public static async Task UpdateEntrySets(int exerciseEntryId, ObservableCollection<int> reps)
        {
            var db = new JuanLogDBContext();
            foreach (Set set in db.SetTable.Where(s => s.EntryId == exerciseEntryId))
            {
                db.SetTable.Remove(set);
            }
            await db.SaveChangesAsync();

            foreach (int repNumber in reps)
            {
                db.SetTable.Add(new Set { Repetitions = repNumber, EntryId = exerciseEntryId });
            }
            await db.SaveChangesAsync();
        }
    }
}

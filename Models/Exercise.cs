using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;



namespace JuanLog.Models
{
    public partial class Exercise
    {
        [Key]
        public string ExerciseName { get; set; }
        public int CategoryId { get; set; }


        public static async Task<List<Exercise>> GetAllExercises()
        {
            var db = new JuanLogDBContext();
            return await db.Exercises.ToListAsync();
        }

        public static async Task<Exercise?> GetExerciseByName(string exerciseName)
        {
            var db = new JuanLogDBContext();
            return await db.Exercises.Where(e => e.ExerciseName == exerciseName).FirstOrDefaultAsync();
        }
    }
}

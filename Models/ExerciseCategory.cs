using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace JuanLog.Models
{
    public partial class ExerciseCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public required string Name { get; set; }

        public static async Task<List<ExerciseCategory>> GetAllCategories()
        {
            var db = new JuanLogDBContext();
           return await db.ExerciseCategories.ToListAsync();
        }
    }
}

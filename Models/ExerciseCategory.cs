using System.ComponentModel.DataAnnotations;

namespace JuanLog.Models
{
    public partial class ExerciseCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public required string Name { get; set; }
    }
}

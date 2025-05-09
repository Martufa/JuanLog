using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace JuanLog.Models
{
    public partial class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }
        public int CategoryId { get; set; }
        public required string Name { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace JuanLog.Models
{
    public partial class ExerciseEntry
    {
        [Key]
        public int EntryId { get; set; }
        public int UserId { get; set; }
        public int ExerciseId { get; set; }
        public DateTime When { get; set; }
        public int Weight { get; set; }

    }
}

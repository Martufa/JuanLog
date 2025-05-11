using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace JuanLog.Models
{
    public partial class Set
    {
        [Key]
        public int SetId { get; set; }
        public int EntryId { get; set; }
        public int Repetitions { get; set; }

    }
}

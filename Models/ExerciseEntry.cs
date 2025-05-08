using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanLog.Models
{
    public class ExerciseEntry
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB; Initial Catalog=JuanLogDB; Integrated Security=true";
        private DateTime _date;
        private string _category;
        private int _weight;
        private int _reps;
        private int _sets;
        public ExerciseEntry(DateTime date, string category, int weight, int reps, int sets)
        {
            if (!ExerciseCategory.ExerciseCategories.Contains(category))
            {
                throw new ArgumentException("Tato kategorie neexistuje, Juane, budeš ji muset vytvořit.");
            }

            _date = date;
            _category = category;
            _weight = weight;
            _reps = reps;
            _sets = sets;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuanLog.Models
{
    public static class ExerciseCategory
    {
        public static HashSet<string> ExerciseCategories = new()
        {
            "Arm strength",
            "Back exercise",
            "Upper leg exercise",
            "Stretching",
            "Funny quirky movement",
        };

        public static bool addCategory(string newCategory)
        {
            return ExerciseCategories.Add(newCategory);
        }

        public static bool removeCategory(string newCategory)
        {
            return ExerciseCategories.Remove(newCategory);
        }
    }
}

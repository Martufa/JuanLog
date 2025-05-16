using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;
using JuanLog.Models;

namespace JuanLog.Messages
{
    public class ExerciseEntryChangedMessage: ValueChangedMessage<ExerciseEntry>
    {
        public ExerciseEntryChangedMessage(ExerciseEntry editedExercise) : base(editedExercise)
        {
            Debug.WriteLine("Exercise changed, sending the new-edited version");
        }
    }
}

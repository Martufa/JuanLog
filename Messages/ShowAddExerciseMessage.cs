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
    public class ShowAddExerciseMessage : ValueChangedMessage<User>
    {
        public ShowAddExerciseMessage(User activeUser) : base(activeUser)
        {
            Debug.WriteLine("going to ADD EXERCISES, message sent");
        }
    }
}

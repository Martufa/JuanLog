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
    public class ShowProgressMessage : ValueChangedMessage<User>
    {
        public ShowProgressMessage(User activeUser) : base(activeUser)
        {
            Debug.WriteLine("going to show PROGRESS, message sent");
        }
    }
}

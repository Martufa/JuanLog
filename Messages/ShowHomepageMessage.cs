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
    public class ShowHomepageMessage : ValueChangedMessage<User>
    {
        public ShowHomepageMessage(User activeUser) : base(activeUser)
        {
            Debug.WriteLine("going home, message sent");
        }
    }
}

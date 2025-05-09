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
    public class ShowImportMessage : ValueChangedMessage<User>
    {
        public ShowImportMessage(User activeUser) : base(activeUser)
        {
            Debug.WriteLine("going to show IMPORT, message sent");
        }
    }
}

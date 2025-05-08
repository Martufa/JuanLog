using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JuanLog.Messages
{
    public class ShowProgressMessage : ValueChangedMessage<object?>
    {
        public ShowProgressMessage() : base(null)
        {
            Debug.WriteLine("going to show PROGRESS, message sent");
        }
    }
}

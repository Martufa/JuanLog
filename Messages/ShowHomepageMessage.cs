using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JuanLog.Messages
{ 
    public class ShowHomepageMessage : ValueChangedMessage<object?>
    {
        public ShowHomepageMessage() : base(null)
        {
            Debug.WriteLine("going home, message sent");
        }
    }
}

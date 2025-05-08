using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JuanLog.Messages
{
    public class ShowImportMessage : ValueChangedMessage<object?>
    {
        public ShowImportMessage() : base(null)
        {
            Debug.WriteLine("going to show IMPORT, message sent");
        }
    }
}

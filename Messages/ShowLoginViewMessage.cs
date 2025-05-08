using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JuanLog.Messages
{
    public class ShowLoginViewMessage : ValueChangedMessage<object?>
    {
        public ShowLoginViewMessage() : base(null) {
            Debug.WriteLine("I want to switch to LOGIIIIN! MSG SENT!");
        }
    }
}

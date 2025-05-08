using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace JuanLog.Messages
{
    public class ShowRegistrationViewMessage : ValueChangedMessage<object?>
    {
        public ShowRegistrationViewMessage() : base(null) {
            Debug.WriteLine("I want to switch to REGISTRATIOOON! MSG SENT!");
        }
    }
}

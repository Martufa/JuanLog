using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;
using JuanLog.ViewModels;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    public partial class AddExerciseViewModel
    {
        public User activeUser { get; set; }
        public AddExerciseViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowHomepageMessage>(this, (r, m) =>
            {
                activeUser = m.Value;
            });
            activeUser = new User();
        }
        [RelayCommand]
        public void ToHomepageCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(activeUser));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    public partial class HomepageViewModel
    {
        [ObservableProperty]
        private string _greeter;
        
        [ObservableProperty]
        private User _activeUser; // { get; set; }
        public HomepageViewModel() {
            WeakReferenceMessenger.Default.Register<ShowHomepageMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value as User;
                Debug.WriteLine("RECEIVED USER!" + m.Value.Name);
                Greeter = $"Oh, můj bože, {ActiveUser.Name} je zde!\nVítej! Co si přeješ dělat?";
            });
            Debug.WriteLine("Serring default");
            _activeUser = new User{ Name="Nikdo"};
            _greeter = $"Ahoj! {ActiveUser.Name}! Co si přeješ dělat?";
        }


        [RelayCommand]
        public void ToImport()
        {
            WeakReferenceMessenger.Default.Send(new ShowImportMessage(_activeUser));
        }

        [RelayCommand]
        public void ToAddExercise()
        {
            WeakReferenceMessenger.Default.Send(new ShowAddExerciseMessage(_activeUser));
        }

        [RelayCommand]
        public void ToLogin()
        {
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }

        [RelayCommand]
        public void ToProfile()
        {
            WeakReferenceMessenger.Default.Send(new ShowProfileMessage(_activeUser));
        }

        [RelayCommand]
        public void ToProgress()
        {
            WeakReferenceMessenger.Default.Send(new ShowProgressMessage(_activeUser));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Views;
using JuanLog.Messages;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    partial class MainWindowViewModel
    {
        UserControl loginPage;
        UserControl registrationPage;
        UserControl homePage;
        UserControl addExercisePage;

        [ObservableProperty]
        UserControl _currentControl;

        public MainWindowViewModel()
        {
            Debug.WriteLine("Well, Main Window initialized");
            loginPage = new LoginView();
            registrationPage = new RegistrationView();
            addExercisePage = new AddExerciseView();
            homePage = new HomepageView();

            _currentControl = loginPage;

            WeakReferenceMessenger.Default.Register<ShowLoginViewMessage>(this, (r, m) =>
            {
                Debug.WriteLine("SWITCHIIING LOGIN!");
                CurrentControl = loginPage;
            });

            WeakReferenceMessenger.Default.Register<ShowRegistrationViewMessage>(this, (r, m) =>
            {
                Debug.WriteLine("SWITCHIIING REGISTER!");
                CurrentControl = registrationPage;
            });

            WeakReferenceMessenger.Default.Register<ShowHomepageMessage>(this, (r, m) =>
            {
                Debug.WriteLine("SWITCHIIING HOME!");
                CurrentControl = homePage;
            });

            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());

        }

        [RelayCommand]
        public void ToLoginCommand() {Debug.WriteLine("Sending the switch message!"); WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage()); }
    }
}

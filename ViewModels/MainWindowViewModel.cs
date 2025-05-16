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
        UserControl importDataPage;
        UserControl profilePage;
        UserControl progressPage;

        [ObservableProperty]
        UserControl _currentControl;

        public MainWindowViewModel()
        {
            Debug.WriteLine("Well, Main Window initialized");
            loginPage = new LoginView();
            registrationPage = new RegistrationView();
            addExercisePage = new AddExerciseView();
            homePage = new HomepageView();
            importDataPage = new ImportDataView();
            profilePage = new ProfileView();
            progressPage = new ProgressView();


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

            WeakReferenceMessenger.Default.Register<ShowAddExerciseMessage>(this, (r, m) =>
            {
                Debug.WriteLine("SWITCHIIING ADD EXERCISES!");
                CurrentControl = addExercisePage;
            });

            WeakReferenceMessenger.Default.Register<ShowImportMessage>(this, (r, m) =>
            {
                Debug.WriteLine("SWITCHIIING IMPORT!");
                CurrentControl = importDataPage;
            });

            WeakReferenceMessenger.Default.Register<ShowProfileMessage>(this, (r, m) =>
            {
                Debug.WriteLine("SWITCHIIING PROFILE!");
                CurrentControl = profilePage;
            });

            WeakReferenceMessenger.Default.Register<ShowProgressMessage>(this, (r, m) =>
            {
                Debug.WriteLine("SWITCHIIING IMPORT!");
                CurrentControl = progressPage;
            });


            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());

        }

        [RelayCommand]
        public void ToLoginCommand() { WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage()); }
    }
}

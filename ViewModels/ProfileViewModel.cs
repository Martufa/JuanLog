using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;
using JuanLog.Views;
using Microsoft.EntityFrameworkCore;

namespace JuanLog.ViewModels
{
    [ObservableObject]
    partial class ProfileViewModel
    {
        [ObservableProperty]
        private User _activeUser;

        // USERNAME
        // POČET CVIKŮ
        // ZMĚNIT HESLO
        // ODSTRANIT ÚČET
        [ObservableProperty]
        private int _numberOfExercises;

        public ProfileViewModel()
        {
            WeakReferenceMessenger.Default.Register<ShowProfileMessage>(this, (r, m) =>
            {
                ActiveUser = m.Value;
                updateNumberOfExercises();
            });
            _activeUser = new User();
            _numberOfExercises = 0;
        }
        private async void updateNumberOfExercises()
        {
            var allEntries = await ExerciseEntry.GetAllUserEntries(ActiveUser);
            NumberOfExercises = allEntries.Count;
        }

        [RelayCommand]
        public void ChangePassword()
        {
            var popUp = new ChangePassword();
            popUp.DataContext = this;
            popUp.Show();
        }

        [RelayCommand]
        public async Task DeleteAccount()
        {
            var db = new JuanLogDBContext();
            db.Users.Remove(ActiveUser);
            await db.SaveChangesAsync();

            MessageBox.Show("Účet úspěšně smazán");
            WeakReferenceMessenger.Default.Send(new ShowLoginViewMessage());
        }

        [RelayCommand]
        public void ToHomepageCommand()
        {
            WeakReferenceMessenger.Default.Send(new ShowHomepageMessage(ActiveUser));
        }

        [RelayCommand]
        public async Task SaveNewPassword(string newPassword)
        {
            var db = new JuanLogDBContext();
            try
            { 
                db.Users.Where(u => u.Id == ActiveUser.Id).First().HashedPassword = User.HashPassword(newPassword);
            }
            catch (Exception)
            {
                MessageBox.Show("Změna hesla se nezdařila...");
            }
            await db.SaveChangesAsync();
        }
    }
}

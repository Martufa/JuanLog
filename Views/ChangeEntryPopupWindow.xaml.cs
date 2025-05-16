using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Messaging;
using JuanLog.Messages;
using JuanLog.Models;
using JuanLog.ViewModels;

namespace JuanLog.Views
{
    /// <summary>
    /// Interaction logic for ChangeEntryPopupWindow.xaml
    /// </summary>
    public partial class ChangeEntryPopupWindow : Window
    {
        public ChangeEntryPopupWindow(ExerciseEntry exerciseEntry)
        {
            InitializeComponent();
            DataContext = new AddExerciseViewModel();
            var d = ((AddExerciseViewModel)this.DataContext);
            d.Entry = exerciseEntry;
            d.UpdateSelectedExerciseFromExName(exerciseEntry.ExerciseName);
            d.UpdateShownSets();
            d.Weight = exerciseEntry.Weight;
            
        }

        public async void ButtonClickChangeEntry(object sender, RoutedEventArgs e)
        {
            var vm = (AddExerciseViewModel)this.DataContext;
            vm.UpdateSets();
            WeakReferenceMessenger.Default.Send(new ExerciseEntryChangedMessage(vm.GetExerciseEntry()));
            this.Close();
        }
    }
}

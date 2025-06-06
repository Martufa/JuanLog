﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using JuanLog.Models;
using JuanLog.ViewModels;

namespace JuanLog.Views
{
    /// <summary>
    /// Interaction logic for ProgressView.xaml
    /// </summary>
    public partial class ProgressView : UserControl
    {
        public ProgressView()
        {
            InitializeComponent();
            DataContext = new ProgressViewModel();
        }

        private void DeleteEntry(object sender, RoutedEventArgs e)
        {
            var vm = (ProgressViewModel)this.DataContext;
            vm.RemoveFromDB((ExerciseEntry)ProgressTableEntry.CurrentItem);            
        }

        private void ChangeEntry(object sender, RoutedEventArgs e)
        {
            var vm = (ProgressViewModel)this.DataContext;
            vm.ChangeEntry((ExerciseEntry)ProgressTableEntry.CurrentItem);
        }
    }
}

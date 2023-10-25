using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading;
using System.Windows.Media;
using H2XC.Core;
using H2XC.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using H2XC.MVVM.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using H2XC.MVVM;
using System.Threading.Tasks;

namespace H2XC
{
    /// <summary>
    /// Shows the main window of the application and handles the UI.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Bar_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        // Allows the user to drag the window around using the top bar.
        private void Bar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        // Minimizes the window.
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Closes the window.
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            btnHome.IsChecked = true;
        }
    }
}

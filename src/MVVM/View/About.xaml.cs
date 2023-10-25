using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;

namespace H2XC.MVVM.View
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();

            // Set current Build Date (dd/mm/yyyy hh:mm)
            string dateTime = Properties.Resources.BuildDate.ToString();
            dateTime = dateTime.Remove(dateTime.Length - 9);
            BDateLbl.Content += dateTime;

            // Set current Version (Major.Minor.Build/Patch)
            VersionLbl.Content += Properties.Resources.Version.ToString();

            // Set current Branch (Public Release, Beta, Alpha, Tester)
            BranchLbl.Content += Properties.Resources.Branch.ToString();
        }

        

        private void BtnGitHub_Click(object sender, RoutedEventArgs e)
        {
            var startInfo = new ProcessStartInfo("cmd", $"/c start https://github.com/Harmless05/H2XC/")
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(startInfo);
        }

        private void btnLicense_Click(object sender, RoutedEventArgs e)
        {
            var startInfo = new ProcessStartInfo("cmd", $"/c start https://github.com/Harmless05/H2XC/blob/main/LICENSE")
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(startInfo);
        }

        private void btnDiscord_Click(object sender, RoutedEventArgs e)
        {
            var startInfo = new ProcessStartInfo("cmd", $"/c start https://discord.com/invite/a2XevWa4zQ")
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(startInfo);
        }

        private void lel_Click(object sender, RoutedEventArgs e)
        {
            // An easter egg. I'm not sure why you're looking at this code. But congrats, you found it! :)
            // It was acutally supposed to be found in the About page.
            var startInfo = new ProcessStartInfo("cmd", $"/c start https://youtube.com/watch?v=1qN72LEQnaU")
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(startInfo);
        }
    }
}

using System;
using System.Windows.Controls;
using System.Net.Http;

namespace H2XC.MVVM.View
{
    /// <summary>
    /// View for the home page that displays the changelog and announcements
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

            //Reused code from my injector
            LoadChangelog();
            LoadAnnouncements();
        }

        // This whole system is used to so the programm doesn't have to request the data from the site every time the user switches tabs

        //Load and cache changelog data
        private static string? cachedChangelogText;
        //Loads the changelog raw text from the site and adds it to the changelog textbox
        private async void LoadChangelog()
        {
            if (cachedChangelogText != null)
            {
                changelog.Text = cachedChangelogText;
                return;
            }

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            string url = "https://harmlessdev.xyz/announcements.txt";
            using HttpClient client = new();
            try
            {
                // Get the response as a string
                string response = await client.GetStringAsync(url);
                changelog.Text = response;
                cachedChangelogText = response;
            }
            catch (HttpRequestException ex)
            {
                // Handle exception if any error occurs during the HTTP request
                changelog.Text = $"Error: {ex.Message}";
            }
        }

        //Load and cache announcements data
        private static string? cachedAnnouncements;
        //Loads the announcements raw text from the site and adds it to the announcements textbox
        private async void LoadAnnouncements()
        {
            if (cachedAnnouncements != null)
            {
                announcements.Text = cachedAnnouncements;
                return;
            }

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            string url = "https://harmlessdev.xyz/announcements.txt";
            using HttpClient client = new();
            try
            {
                // Get the response as a string
                string? response = await client.GetStringAsync(url);
                announcements.Text = response ?? "";
                cachedAnnouncements = response;
            }
            catch (HttpRequestException ex)
            {
                // Handle exception if any error occurs during the HTTP request
                announcements.Text = $"Error: {ex.Message}";
            }
        }

        //Remove cached data on exit
        private static void OnProcessExit(object? sender, EventArgs e)
        {
            if (cachedAnnouncements != null)
            {
                cachedAnnouncements = null;
            }
            cachedChangelogText = null;
        }
    }
}

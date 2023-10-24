using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;


namespace H2XC.MVVM.View
{
    /// <summary>
    /// View to get public IP address information and display it in a DataGrid
    /// </summary>
    public partial class IP2ADRS : UserControl
    {
        public IP2ADRS()
        {
            InitializeComponent();
        }

        // Get public IP address information
        private async void GetInfo()
        {
            string strIpLocation = string.Empty;

            string ip = txtIP.Text;
            string url = "http://ip-api.com/json/" + ip + "?fields=status,message,country,regionName,city,zip,lat,lon,timezone,currency,isp,org,as,reverse,mobile,proxy,hosting";
            using (HttpClient client = new HttpClient())
            {
                string responseJson = await client.GetStringAsync(url);
                var ipInfo = JsonConvert.DeserializeObject<IDictionary>(responseJson);
                var dataList = new List<DictionaryEntry>();
                //txtJson.Text = JsonConvert.SerializeObject(ipInfo, Formatting.Indented);

                foreach (var key in ipInfo.Keys)
                {
                    string customKey = key.ToString().Substring(0, 1).ToUpper() + key.ToString().Substring(1);
                    dataList.Add(new DictionaryEntry(customKey, ipInfo[key]));
                }

                datagrid.ItemsSource = dataList;
            }
        }

        // Call GetInfo() method
        private void btnGetInfo_Click(object sender, RoutedEventArgs e)
        {
            if (txtIP.Text == string.Empty)
            {
                MessageBox.Show("Please enter an IP address", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                GetInfo();
            }
        }

        // Copy last clipboard entry and paste it into the IP address textbox
        private void btnPaste_Click(object sender, RoutedEventArgs e)
        {
            string clipboard = Clipboard.GetText();// Get the text from clipboard
            clipboard = clipboard.Replace("http://", "").Replace("https://", ""); // Remove http:// and https:// from the string
            clipboard = Regex.Replace(clipboard, "[^0-9.]+", ""); // Remove all non-numeric characters
            txtIP.Text = clipboard;
            //txtIP.Text = remhttp.Substring(0, Math.Min(15, remhttp.Length)); // Math.Min() to handle cases where the string length is less than 15
        }

        // Allows user to press Enter key to call GetInfo() method
        private void txtIP_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetInfo();
            }
        }
    }
}

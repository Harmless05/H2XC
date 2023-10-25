using System.Windows.Controls;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Windows;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using System.Net.Http;
namespace H2XC.MVVM.View
{
    /// <summary>
    /// View to get local and public IP address information and display public IP address information in a DataGrid
    /// </summary>
    public partial class LocInfo : UserControl
    {
        public LocInfo()
        {
            InitializeComponent();
            GetLocalIPAddress();
            GetPublicIPAddress();
        }

        // Get local IP address
        public string GetLocalIPAddress()
        {
            string localIP = null;
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.GetIPProperties().GatewayAddresses.Count > 0)
                {
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                LocalIP.Text = ip.Address.ToString();
                                break;
                            }
                        }
                    }
                }
                if (localIP != null)
                {
                    break;
                }
            }
            return localIP ?? "(Unknown)";
        }

        // Get public IP address
        public string GetPublicIPAddress()
        {
            string publicIP = null;
            try
            {
                publicIP = new WebClient().DownloadString("https://api.ipify.org");
                PublicIP.Text = publicIP;
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to get public IP address. Please check your internet connection.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return publicIP ?? "(Unknown)";
        }

        // Get location info
        private async void GetInfo()
        {
            string strIpLocation = string.Empty;

            string ip = PublicIP.Text;
            string url = "http://ip-api.com/json/" + ip + "?fields=status,message,country,regionName,city,zip,lat,lon,timezone,currency,isp,org,as,reverse,mobile,proxy,hosting";
            using HttpClient client = new HttpClient();
            string responseJson = await client.GetStringAsync(url);
            var ipInfo = JsonConvert.DeserializeObject<IDictionary>(responseJson);
            var dataList = new List<DictionaryEntry>();

            foreach (var key in ipInfo.Keys)
            {
                string customKey = key.ToString().Substring(0, 1).ToUpper() + key.ToString().Substring(1);
                dataList.Add(new DictionaryEntry(customKey, ipInfo[key]));
            }

            datagrid.ItemsSource = dataList;
        }

        // Call GetInfo() method
        private void btnSeePublicInfo_Click(object sender, RoutedEventArgs e)
        {
            GetInfo();
        }
    }
}

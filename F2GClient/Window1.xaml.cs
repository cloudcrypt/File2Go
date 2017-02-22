using System;
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
using System.Windows.Shapes;

namespace F2GClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            DeviceName.Content = System.Net.Dns.GetHostName();
            IPAddress.Content = getMacAddress();
        }

        private string getMacAddress()
        {
            string hostName = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            LoginWindow win2 = new LoginWindow();
            win2.Show();
            this.Close();
        }
    }
}

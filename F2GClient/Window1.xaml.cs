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
using F2G;

namespace F2GClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool actualClose = false;

        public Window1()
        {
            InitializeComponent();
            DeviceName.Content = System.Net.Dns.GetHostName();
            IPAddress.Content = getMacAddress();


            System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();
            icon.Icon = new System.Drawing.Icon("F2GIMG.ico");
            icon.Visible = true;
            icon.DoubleClick += new EventHandler(icon_click);


        }

        private void icon_click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void Window1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (actualClose == false)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                // Do nothing

            }
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
            actualClose = true;
            this.Close();
        }
    }
}

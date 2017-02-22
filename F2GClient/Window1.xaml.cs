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
using F2G.Models;

namespace F2GClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool actualClose = false;
        private User user;
        Client mycomputer;

        public Window1(User user)
        {
            InitializeComponent();
            DeviceName.Content = System.Net.Dns.GetHostName();
            IPAddress.Content = getMacAddress();


            System.Windows.Forms.NotifyIcon icon = new System.Windows.Forms.NotifyIcon();
            icon.Icon = new System.Drawing.Icon("F2GIMG.ico");
            icon.Visible = true;
            icon.DoubleClick += new EventHandler(icon_click);
            icon.ContextMenu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem item;
            icon.ContextMenu.MenuItems.Add(item = new System.Windows.Forms.MenuItem("Quit"));
            item.Click += Item_Click;

            this.user = user;
            identifyClient(user);

            startListening();
        }

        private void startListening()
        {
            
        }

        private void Item_Click(object sender, EventArgs e)
        {
            actualClose = true;
            this.Close();
        }

        private void identifyClient(User user)
        {
            using (F2GContext db = new F2GContext())
            {
                String hostname = System.Net.Dns.GetHostName();
                String Ip = System.Net.Dns.GetHostByName(hostname).AddressList[0].ToString();

                mycomputer = new Client() { name = hostname, ip = Ip,  User = user};

                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                db.Clients.Add(mycomputer);
                db.SaveChanges();
            }
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
                removeClient();
                // Do nothing

            }
        }

        private void removeClient()
        {
            using (F2GContext db = new F2GContext())
            {
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                db.Clients.Remove(mycomputer);
                db.SaveChanges();
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

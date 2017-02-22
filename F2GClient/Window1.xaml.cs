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
using System.Threading;

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

            fillLabels();

            startListening();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e) {
            
        }

        private void fillLabels()
        {
            DeviceName.Content = System.Net.Dns.GetHostName();
            IPAddress.Content = getMacAddress();
            Name.Content = user.fname + "  " +  user.lname;
            Email.Content = user.email;
        }

        internal void displayConnection()
        {
            Status.Content = "Connection request recieved, Searching and sending files"; 
        }

        private void startListening()
        {
            var bc = new BrushConverter();
            ConnectionStatusLabel.Background =(Brush)bc.ConvertFrom("#FF89F084");
            ConnectionStatusLabel.Content = "Connected";
            try
            {
                F2GDBListner listen = new F2GDBListner(IPAddress.Content.ToString(), this);
                listen.FileFound += Listen_FileFound;
                listen.CheckQueue();
            }
            catch (Exception e)
            {
                var bc2 = new BrushConverter();
                ConnectionStatusLabel.Background = (Brush)bc2.ConvertFrom("#FFF5EBEB");
                ConnectionStatusLabel.Content = "Not Connected";
            }
        }

        private void Listen_FileFound(object sender, F2GDBListner.FileFoundEventArgs e)
        {
            string path = Search4File.Search(e.RequestData.fileName).Split('\n')[0];
            byte[] file = File2Bytes.ConvertFileToBytes(path);
            using (F2GContext db = new F2GContext())
            {
                Response rsp;
                if (file != null)
                {
                    Request rsq = e.RequestData;
                    db.Entry(rsq).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    rsp = new Response() { success = true, request = rsq };
                    db.Responses.Add(rsp);
                    db.Files.Add(new File() { name = e.RequestData.fileName, contents = file, uploaded =  DateTime.Now, response = rsp });
                    db.SaveChanges();
                    return;
                }
                rsp = new Response() { success = false, request = e.RequestData };
                db.Responses.Add(rsp);
                db.SaveChanges();
                return;
            }
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


                try
                {
                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    db.Clients.Add(mycomputer);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    //Status.Content = "Something Went Wrong adjusting DataBase";
                    //removeClient();
                    //identifyClient(user);
                }
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
            try
            {
                using (F2GContext db = new F2GContext())
                {
                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    db.Clients.Remove(mycomputer);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                // Do nothing for Database exception // 
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

        private void attemptReconnect(object sender, MouseButtonEventArgs e)
        {
            if (label.Content == "Not Connected".ToString())
            {
                startListening();
            }
        }
    }
}

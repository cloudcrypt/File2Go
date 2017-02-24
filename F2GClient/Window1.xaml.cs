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

        internal void updateStatus(string v)
        {
            Status.Content = v; 
        }

        private void startListening()
        {
            this.Dispatcher.Invoke(() =>
            {
                var bc = new BrushConverter();
                ConnectionStatusLabel.Background = (Brush)bc.ConvertFrom("#FF89F084");
                ConnectionStatusLabel.Content = "Connected";
                string ip = IPAddress.Content.ToString();
                Console.WriteLine("in startListening");
                F2GDBListner listen = new F2GDBListner(this, ip);
                listen.FileFound += Listen_FileFound;
                listen.CheckQueue();
            });
            //try
            //{
                //Console.WriteLine("in startListening");
                //F2GDBListner listen = new F2GDBListner(this, ip);
                //listen.FileFound += Listen_FileFound;
                //listen.CheckQueue();
            //}
            //catch (Exception e)
            //{
            //    var bc2 = new BrushConverter();
            //    ConnectionStatusLabel.Background = (Brush)bc2.ConvertFrom("#FFF5EBEB");
            //    ConnectionStatusLabel.Content = "Not Connected";
            //}
        }

        private void Listen_FileFound(object sender, F2GDBListner.FileFoundEventArgs e)
        {
            Console.WriteLine("in FileFound");
            string[] paths = Search4File.Search(e.RequestData.fileName).Split('\n');
            List<Tuple<string, byte[]>> files = new List<Tuple<string, byte[]>>();
            foreach (string path in paths)
            {
                files.Add(new Tuple<string, byte[]>(path, File2Bytes.ConvertFileToBytes(path)));
            }
            files = files.Where(f => f != null).ToList();
            using (F2GContext db = new F2GContext())
            {
                Request rsq = e.RequestData;
                db.Entry(rsq).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                User user = rsq.User;
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                Client client = rsq.client;
                db.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                Response rsp = new Response() { success = (files.Count > 0), fileName = rsq.fileName, User = user, client = client };
                db.Responses.Add(rsp);
                foreach (Tuple<string, byte[]> file in files)
                {
                    Console.WriteLine("sending a file!");
                    db.Files.Add(new File() { name = e.RequestData.fileName, path = file.Item1, contents = file.Item2, response = rsp });
                }
                db.Requests.Remove(rsq);
                db.SaveChanges();
            }
            startListening();
        }
                //using (F2GContext db = new F2GContext())
                //{
                //    Request rsq = e.RequestData;
                //    Response rsp;
                //    db.Entry(rsq).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                //    User user = rsq.User;
                //    Client client = rsq.client;
                //    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                //    db.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                //    if (file != null)
                //    {
                //        Console.WriteLine("sending a file!");
                //        rsp = new Response() { success = true, fileName = rsq.fileName, User = user, client = client };
                //        db.Responses.Add(rsp);
                //        db.Files.Add(new File() { name = e.RequestData.fileName, path = path, contents = file, response = rsp });
                //        //db.Requests.Remove(rsq);
                //        db.SaveChanges();
                //        //startListening();
                //    } else
                //    {
                //        rsp = new Response() { success = false, User = user, client = client };
                //        db.Responses.Add(rsp);
                //        //db.Requests.Remove(rsq);
                //        db.SaveChanges();
                //    }
                //    //startListening();
                //}
            //}
            //using (F2GContext db = new F2GContext())
            //{
            //    Request rsq = e.RequestData;
            //    db.Requests.Remove(rsq);
            //    db.SaveChanges();
            //}
            //startListening();
        //}

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

                mycomputer = new Client() { name = hostname, ip = Ip, active = true,  User = user};
                List<Client> existingClients = db.Clients.Where(c => c.ip == Ip).ToList();
                foreach (Client existing in existingClients)
                {
                    existing.active = false;
                }
                db.SaveChanges();
                Client existingClient = db.Clients.FirstOrDefault(c => c.ip == Ip);
                if (existingClient == null)
                {
                    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                    db.Clients.Add(mycomputer);
                } else
                {
                    existingClient.active = true;
                }
                db.SaveChanges();

                //try
                //{
                //    db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                //    db.Clients.Add(mycomputer);
                //    db.SaveChanges();
                //    Status.Content = "Sucessfully connected to DataBase";
                //    //db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                //    //db.Clients.Add(mycomputer);
                //    //db.SaveChanges();
                //    //Status.Content = "Sucessfully connected to DataBase"; 
                //}
                //catch (Exception e)
                //{
                //    Status.Content = "Something Went Wrong adjusting DataBase";
                //    removeClient();
                //    removeClient();
                //    identifyClient(user);
                //}
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
            //try
            //{
            using (F2GContext db = new F2GContext())
            {
                //db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                //db.Clients.Remove(mycomputer);
                db.Entry(mycomputer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                mycomputer.active = false;
                db.SaveChanges();
            }
            //}
            //catch (Exception e)
            //{

            //    // Do nothing for Database exception // 
            //}
        }
        private string getMacAddress()
        {
            string hostName = System.Net.Dns.GetHostName();
            string myIP = System.Net.Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            removeClient();
            LoginWindow win2 = new LoginWindow();
            win2.Show();
            actualClose = true;
            this.Close();
        }

    }
}

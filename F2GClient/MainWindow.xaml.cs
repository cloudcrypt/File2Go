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
using System.Windows.Navigation;
using System.Windows.Shapes;
using F2G.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace F2GClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            //textBlock.Text = F2G.Class1.str;
        }

        private void LoginAttempt(object sender, RoutedEventArgs e)
        {

            //User userAttempt = new User(new Tuple<string,string>(Emailblock.Text.Trim(), getHash(Passwordblock.Password.Trim())));
            using (F2GContext db = new F2GContext())
            {
                User user = db.Users.FirstOrDefault(u => u.email == Emailblock.Text.Trim());
                if (user == null) { Console.WriteLine("User does not exist!"); return; }
                if (user.hash == getHash(Passwordblock.Password.Trim()))
                {
                    Console.WriteLine("user variable is now authenticated user!");
                    identifyClient(user);
                    return;
                }
                Console.WriteLine("incorrect username or password!");
            }
        }

        private void identifyClient(User user)
        {
            using (F2GContext db = new F2GContext())
            {
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                db.Clients.Add(new Client() { name = "BLAH-PC", ip = "192.168.2.1", User = user });
                db.SaveChanges();
            }
        }

        private static string getHash(string pswd)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(pswd);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }


        private void openMain()
        {
            Window1 win1 = new Window1();
            win1.Show();
            this.Close();


        }

        private void no_account(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://file2go.azurewebsites.net/Account/Register");
        }

        private void no_account_Under(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            dhana.Foreground = (Brush)bc.ConvertFrom("#FF201C64"); 
        }

        private void no_account_over(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            dhana.Foreground = (Brush)bc.ConvertFrom("#FFA20F0F");
        }
    }
}

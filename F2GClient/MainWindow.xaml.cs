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

            User userAttempt = new F2G.Models.User(Emailblock.Text.Trim(), Passwordblock.Password.Trim());



        }

        private void checkPassword()
        {

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

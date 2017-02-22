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
            openMain();
           /*string input = Emailblock.Text.Trim();
           if (input == "Hello")
            {
                checkPassword();

            }
            else
            {
                InvalidLbl.Visibility = Visibility.Visible; 

            }*/
        }

        private void checkPassword()
        {
            String input = Passwordblock.Password.Trim();
           /*if (input == "World")
            {
                Console.Write("Login Success");
                openMain();
            }
            else
            {
                InvalidLbl.Visibility = Visibility.Visible;

            }*/
        }

        private void openMain()
        {
            Window1 win1 = new Window1();
            win1.Show();
            this.Close();


        }

        private void no_account(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

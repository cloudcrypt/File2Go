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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //textBlock.Text = F2G.Class1.str;
        }

        private void LoginAttempt(object sender, RoutedEventArgs e)
        {
            if (Emailblock.Text == "Hello")
            {
                checkPassword();

            }
            else
            {
                InvalidLbl.Visibility = Visibility.Visible; 

            }
        }

        private void checkPassword()
        {
            if (Passwordblock.Text == "World")
            {
                Console.Write("Login Success");

            }
            else
            {
                InvalidLbl.Visibility = Visibility.Visible;

            }
        }
    }
}

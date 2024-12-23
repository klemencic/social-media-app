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

namespace UporabniskiVmesniki_Klemencic
{
    /// <summary>
    /// Interaction logic for AddPrijatelji.xaml
    /// </summary>
    public partial class AddPrijatelji : Window
    {
        private MainViewModel main;


        public AddPrijatelji(MainViewModel main)
        {
            InitializeComponent();
            
            this.main = main;
            UpdateLayout();

        }
        private void OK_ClickUser(object sender, RoutedEventArgs e)
        {
            string ime = Ime.Text;
            string priimek = Priimek.Text;

            MainWindow.User user = new MainWindow.User(ime, priimek, "Online");

            main.prijatelji.Add(user);

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}

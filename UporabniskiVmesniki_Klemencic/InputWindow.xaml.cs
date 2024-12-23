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

namespace UporabniskiVmesniki_Klemencic
{
   
    public partial class InputWindow : Window
    {
        private MainViewModel main;
        BitmapImage imageContainer;

        public InputWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            main = viewModel;
            CBox.ItemsSource = main.prijatelji;
            UpdateLayout();


        }
       

        private void Click_AddObjava(object sender, RoutedEventArgs e)
        {
            string inputVsebina = Vsebina.Text;
            string tema = Tema.Text;
            var user = CBox.SelectedItem as MainWindow.User;
            if(user == null)
            {
                MainWindow.Objava objava2 = new MainWindow.Objava(new MainWindow.User("Matej", "Klemencic", "Online"), inputVsebina, DateTime.Now, tema, imageContainer, "None", "");
                main.objave.Add(objava2);
                this.Close();
            }
            else
            {

            MainWindow.Objava objava = new MainWindow.Objava(new MainWindow.User("Matej", "Klemencic", "Online"), inputVsebina, DateTime.Now, tema, imageContainer, user.Ime, "");

            main.objave.Add(objava);

            this.Close();
            }

        }

      

        private void Click_cancel(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private BitmapImage LoadImage(string filePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            return bitmap;
        }

        private void Click_ChooseImage(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Slika"; 
            dlg.DefaultExt = ".jpg"; 
            dlg.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*"; 

            Nullable<bool> result = dlg.ShowDialog();

            string filename = dlg.FileName;
            if (result == true)
            {
                BitmapImage image = LoadImage(filename);
                imageContainer = image;
                selectedImage.Source = imageContainer;

            }





        }
    }
}

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
    /// <summary>
    /// Interaction logic for EditObjava.xaml
    /// </summary>
    public partial class EditObjava : Window
    {
        private MainViewModel main;
        BitmapImage imageContainer;
        public EditObjava(MainViewModel viewModel)
        {
            InitializeComponent();
            main = viewModel;
            CBox.ItemsSource = main.prijatelji;
            UpdateLayout();
        }

        private void Click_EditObjava(object sender, RoutedEventArgs e)
        {
           var selectedPost = main.SelectedPost;

            string novaVsebina = Vsebina.Text;
            string novaTema = Tema.Text;
            
            var user = CBox.SelectedItem as MainWindow.User;
            selectedPost.Slika = imageContainer;

            selectedPost.Vsebina = novaVsebina;
           selectedPost.Tema = novaTema;

            if(user != null)
            {
               selectedPost.TagedFriends = user.Ime;

            }

            main.RefreshListView();
            UpdateLayout(); 

            this.Close();

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

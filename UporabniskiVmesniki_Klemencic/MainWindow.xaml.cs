using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static UporabniskiVmesniki_Klemencic.MainWindow;

namespace UporabniskiVmesniki_Klemencic
{


    public partial class MainWindow : Window
    {
        private TextBox[] textboxes;
        private MainViewModel _viewModel;
        private BitmapImage imageContainer;


        public MainWindow()
        {
            

            InitializeComponent();
            _viewModel = new MainViewModel();
             DataContext = _viewModel;
            _viewModel.OpenInputWindowCommand = new RelayCommand(_viewModel.OpenInputWindow);
            StartPointAnimation();


        }

      
        private void PostListView_DoubleClick(object sender,     RoutedEventArgs e)
        {
            _viewModel.SelectedPost = postListView.SelectedItem as Objava;
            _viewModel.ShowSelectedPostContent();

        }

        private void Uvozi_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Uvozi();
        }

        private void Izvozi_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Izvozi();
        }

        private void EditFriend(object sender, RoutedEventArgs e)
        {
            User selectedUser = prijateljiList.SelectedItem as User;
            _viewModel.EditUser(selectedUser);
           
        }

        private void OpenEditObjava(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedPost = postListView.SelectedItem as Objava;
            _viewModel.OpenEditObjava();
        }

        private void DeleteFriend(object sender, RoutedEventArgs e)
        {
            User selectedUser = prijateljiList.SelectedItem as User;
            _viewModel.DeleteSelectedUser(selectedUser);
        }
        private void Izhod(object sender, RoutedEventArgs e)
        {
            _viewModel.Izhod(); 
        }
       

        private void OpenInputWindow_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenInputWindow();
        }

        private void OpenNastavitve(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenNastavitve();
        }

        private void AddPrijatelji_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenAddPrijatelji();
        }


        private void RefreshListView(object sender, RoutedEventArgs e)
        {
            _viewModel.RefreshListView();
        }

        private void DeleteSelectedPost(object sender, RoutedEventArgs e)
        {
            Objava selectedPost = postListView.SelectedItem as Objava;
            _viewModel.DeleteSelectedPost(selectedPost);
        }

        private void ShowVsebinaViaButton(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowVsebinaViaButton();
        }

        

        private void prijateljiList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


       

        private string GetRelativePath(string absolutePath)
        {
            Uri absoluteUri = new Uri(absolutePath, UriKind.Absolute);
            Uri currentUri = new Uri(Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar, UriKind.Absolute);

            Uri relativeUri = currentUri.MakeRelativeUri(absoluteUri);
            return Uri.UnescapeDataString(relativeUri.ToString());
        }

        private void OsebniPodatki_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void OsebniPodatki_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void OsebniPodatki_Loaded_2(object sender, RoutedEventArgs e)
        {

        }

        private void postListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StartPointAnimation()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = -50; // Initial position off-screen above
            animation.To = 0;
            animation.Duration = TimeSpan.FromSeconds(2);

            TranslateTransform translateTransform = welcomeText.RenderTransform as TranslateTransform;
            if (translateTransform != null)
            {
                translateTransform.BeginAnimation(TranslateTransform.YProperty, animation);
            }
        }

        public void OriginalPostavitev(object sender, RoutedEventArgs e)
        {
            normalview.Visibility = Visibility.Visible;
            alternateview.Visibility = Visibility.Hidden;
        }

        public void AlternativnaPostavitev(object sender, RoutedEventArgs e)
        {
            normalview.Visibility = Visibility.Hidden;
            alternateview.Visibility = Visibility.Visible;
        }
    }
}
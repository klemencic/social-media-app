using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static UporabniskiVmesniki_Klemencic.MainWindow;

namespace UporabniskiVmesniki_Klemencic
{
    
    public partial class Nastavitve : Window
    {
        private MainViewModel _viewModel;

        public Nastavitve(MainViewModel main)
        {
            this._viewModel = main;
            InitializeComponent();
            if(_viewModel.isAutoSaveEnabled == true)
            {
                toggleButton.Content = "ON";
            }
            else if(_viewModel.isAutoSaveEnabled == false)
            {
                toggleButton.Content = "OFF";
            }

           
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.isAutoSaveEnabled = !_viewModel.isAutoSaveEnabled;
            if(_viewModel.isAutoSaveEnabled == true)
            {
                toggleButton.Content = "ON";
            }
            else if(_viewModel.isAutoSaveEnabled == false)
            {
                toggleButton.Content = "OFF";
            }

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            string jsonString = Properties.Settings.Default.prijatelji;

            if (!string.IsNullOrEmpty(jsonString))
            {
                List<User> myList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(jsonString);

                _viewModel.prijatelji = new ObservableCollection<User>(myList);

            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.isAutoSaveEnabled == true)
            {
                List<User> prijateljiList = new List<User>();

                foreach (User user in _viewModel.prijatelji)
                {
                    prijateljiList.Add(user);
                }

                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(prijateljiList);

                Properties.Settings.Default.prijatelji = jsonString;
                Properties.Settings.Default.Save();
            }
        }


        private void sliderValue(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _viewModel.autoSaveTimer.Interval = TimeSpan.FromSeconds((int)slider.Value);
        }





    }
}

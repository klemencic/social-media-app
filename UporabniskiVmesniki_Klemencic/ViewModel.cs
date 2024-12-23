using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;
using static UporabniskiVmesniki_Klemencic.MainWindow;
using System.Windows.Input;
using System.Configuration;
using Microsoft.Win32;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace UporabniskiVmesniki_Klemencic
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private User dataModel;

        public User DataModel
        {
            get { return dataModel; }
            set
            {
                if (dataModel != value)
                {
                    dataModel = value;
                    OnPropertyChanged(nameof(DataModel));
                }
            }
        }

        private ObservableCollection<Objava> _objave;

        public ObservableCollection<Objava> objave
        {
            get { return _objave; }
            set
            {
                _objave = value;
                OnPropertyChanged(nameof(objave));
            }
        }
        private ObservableCollection<User> _prijatelji;

        public ObservableCollection<User> prijatelji
        {
            get { return _prijatelji; }
            set
            {
                _prijatelji = value;
                OnPropertyChanged(nameof(prijatelji));
            }
        }

        private Objava _selectedPost;


        public Objava SelectedPost
        {
            get { return _selectedPost; }
            set
            {
                _selectedPost = value;
                OnPropertyChanged(nameof(SelectedPost));
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        public ICommand OpenInputWindowCommand { get; set; }

        public bool isAutoSaveEnabled = true;
        public DispatcherTimer autoSaveTimer;

        public MainViewModel()
        {
            prijatelji = new ObservableCollection<User>();
            objave = new ObservableCollection<Objava>();
            OpenInputWindowCommand = new RelayCommand(OpenInputWindow);


            autoSaveTimer = new DispatcherTimer();
            autoSaveTimer.Tick += AutoSaveTimer_Tick;
            autoSaveTimer.Interval = TimeSpan.FromMinutes(2);
            autoSaveTimer.Start();
            LoadPrijatelji();
            LoadObjave();


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void AutoSaveTimer_Tick(object sender, EventArgs e)
        {
            SavePrijatelji();
            SaveObjave();
           
        }



        private void LoadPrijatelji()
        {
            string jsonString = Properties.Settings.Default.prijatelji;

            if (!string.IsNullOrEmpty(jsonString))
            {
                List<User> myList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(jsonString);

                prijatelji = new ObservableCollection<User>(myList);

            }
        }

        private void LoadObjave()
        {
            string jsonString = Properties.Settings.Default.objave;

            if (!string.IsNullOrEmpty(jsonString))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Converters.Add(new BitmapImageConverter());

                List<Objava> myList = JsonConvert.DeserializeObject<List<Objava>>(jsonString, settings);
                objave = new ObservableCollection<Objava>(myList);
            }
        }

        private void SavePrijatelji()
        {
            if (isAutoSaveEnabled)
            {
                List<User> prijateljiList = new List<User>();

                foreach (User user in prijatelji)
                {
                    prijateljiList.Add(user);
                    
                }

                string jsonString = JsonConvert.SerializeObject(prijateljiList);

                Properties.Settings.Default.prijatelji = jsonString;
                Properties.Settings.Default.Save();
            }
        }
        private BitmapImage ConvertByteArrayToImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return null;

            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.CreateOptions = BitmapCreateOptions.None; // Add this line
                image.StreamSource = stream;
                image.EndInit();
                image.Freeze(); // Freeze the image to prevent further modifications
            }

            return image;
        }
        private byte[] ConvertImageToByteArray(BitmapImage image)
        {
            if (image == null)
                return null;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
        private void SaveObjave()
        {
            if (isAutoSaveEnabled)
            {
                List<Objava> objavaList = new List<Objava>(objave);

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Converters.Add(new BitmapImageConverter());

                string jsonString = JsonConvert.SerializeObject(objavaList, settings);

                Properties.Settings.Default.objave = jsonString;
                Properties.Settings.Default.Save();
            }
        }

        public void StopAutoSaveTimer()
        {
            if (autoSaveTimer != null)
            {
                autoSaveTimer.Stop();
                autoSaveTimer.Tick -= AutoSaveTimer_Tick;
            }
        }


        public void OpenInputWindow()
        {
            InputWindow inputWindow = new InputWindow(this);
            inputWindow.ShowDialog();
        }

        public void OpenAddPrijatelji()
        {
            AddPrijatelji addPrijateljiwindow = new AddPrijatelji(this);

            addPrijateljiwindow.ShowDialog();
        }

        public void OpenEditObjava()
        {
            EditObjava editObjavaPage = new EditObjava(this);

            if (SelectedPost != null && !editObjavaPage.IsVisible)
            {
                
                
                editObjavaPage.Show();

                

            }

        }


        public void ShowSelectedPostContent()
        {
            if (SelectedPost != null)
            {
                MessageBox.Show(SelectedPost.Vsebina, "Vsebina:", MessageBoxButton.OK);
            }
        }
        public void Uvozi()
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML datoteke (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == true)
            {
                LoadData(openFileDialog.FileName);
            }
        }

        public void Izvozi()
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML datoteke (*.xml)|*.xml";

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveData(saveFileDialog.FileName);
            }
        }

        public void LoadData(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));

                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    DataModel = (User)serializer.Deserialize(fileStream);
                }

                MessageBox.Show("Podatki so bili uspešno naloženi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napaka pri nalaganju podatkov: {ex.Message}");
            }
        }
        public void OpenNastavitve()
        {
            Nastavitve nastavitve = new Nastavitve(this);

            nastavitve.Show();
        }
        public void SaveData(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(User));

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fileStream, DataModel);
                }

                MessageBox.Show("Podatki so bili uspešno shranjeni.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napaka pri shranjevanju podatkov: {ex.Message}");
            }
        }

        public void EditUser(User selectedUser)
        {
            if (selectedUser != null)
            {
               selectedUser.Ime = "STATIC_VALUE";
               selectedUser.Priimek = "STATIC_VALUE";
               selectedUser.Status = "Offline";
            }
            OnPropertyChanged(nameof(prijatelji));
        }
       
       

        public void DeleteSelectedUser(User selectedUser)
        {
            if (selectedUser != null)
            {
                prijatelji.Remove(selectedUser);
            }
        }
        public void Izhod()
        {
            Application.Current.Shutdown();
        }
        

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshListView()
        {
            OnPropertyChanged(nameof(objave));
            OnPropertyChanged(nameof(prijatelji));

        }

        public void DeleteSelectedPost(Objava selectedPost)
        {
            if (selectedPost != null)
            {
                objave.Remove(selectedPost);
            }
        }

        public class IgnoreJsonResolver : DefaultContractResolver
        {
            private readonly HashSet<string> propertiesToIgnore;

            public IgnoreJsonResolver(params string[] propNamesToIgnore)
            {
                propertiesToIgnore = new HashSet<string>(propNamesToIgnore, StringComparer.OrdinalIgnoreCase);
            }

            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                JsonProperty property = base.CreateProperty(member, memberSerialization);
                if (propertiesToIgnore.Contains(property.PropertyName))
                {
                    property.ShouldSerialize = instance => { return false; };
                }
                return property;
            }
        }

        public void ShowVsebinaViaButton()
        {
            if (SelectedPost != null)
            {
                MessageBox.Show(SelectedPost.Vsebina, "Vsebina:", MessageBoxButton.OK);
            }
        }


        private BitmapImage LoadImageFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                BitmapImage image = new BitmapImage(new Uri(filePath));
                image.Freeze();  // Freeze the image to prevent further modifications
                return image;
            }

            return null;
        }

        private string GetImagePath()
        {
            // Use a consistent path to save the images, e.g., in the application directory
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Image_{Guid.NewGuid()}.jpg");
        }

        private void SaveImageToFile(BitmapImage image)
        {
            if (image != null)
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (FileStream stream = new FileStream(GetImagePath(), FileMode.Create))
                {
                    encoder.Save(stream);
                }
            }
        }



    }
}

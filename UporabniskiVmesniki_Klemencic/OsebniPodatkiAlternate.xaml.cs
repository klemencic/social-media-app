using System;
using System.Collections.Generic;
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

namespace UporabniskiVmesniki_Klemencic
{
    
    public partial class OsebniPodatki2 : UserControl
    {
        private TextBox[] textboxes;
        private BitmapImage imageContainer;
        private MainViewModel _viewModel;
        public OsebniPodatki2()
        {
            
            InitializeComponent();
            textboxes = new TextBox[] { Ime, Priimek, Naslov, Drzava, GSM };
            LoadTextBoxContent();
            LoadImageContent();

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
            }

            profileImage.Source = imageContainer;



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

        private void toggleHelp(object sender, RoutedEventArgs e)
        {
            if (help1.Visibility == Visibility.Visible && help2.Visibility ==Visibility.Visible)
            {
                help1.Visibility = Visibility.Hidden;
                help2.Visibility = Visibility.Hidden;
            }
            else
            {
               help1.Visibility = Visibility.Visible;
               help2.Visibility = Visibility.Visible;
            }
        }
        private void SaveImageContent()
        {

            BitmapSource bitmapSource = (BitmapSource)profileImage.Source; // profileImage is your Image control
            JpegBitmapEncoder encoder = new JpegBitmapEncoder(); // or PngBitmapEncoder
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

            byte[] imageBytes;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                imageBytes = memoryStream.ToArray();
            }

            string imageString = Convert.ToBase64String(imageBytes);
            Properties.Settings.Default.ImageBytes = imageString;
            Properties.Settings.Default.Save();

        }

        private string GetRelativePath(string absolutePath)
        {
            Uri absoluteUri = new Uri(absolutePath, UriKind.Absolute);
            Uri currentUri = new Uri(Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar, UriKind.Absolute);

            Uri relativeUri = currentUri.MakeRelativeUri(absoluteUri);
            return Uri.UnescapeDataString(relativeUri.ToString());
        }
        private void LoadImageContent()
        {
            string savedImageString = Properties.Settings.Default.ImageBytes;

            if (!string.IsNullOrEmpty(savedImageString))
            {
                savedImageString = savedImageString.Trim();

                int mod4 = savedImageString.Length % 4;
                if (mod4 > 0)
                {
                    savedImageString += new string('=', 4 - mod4);
                }

                try
                {
                    byte[] savedImageBytes = Convert.FromBase64String(savedImageString);

                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream memoryStream = new MemoryStream(savedImageBytes))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                    }

                    profileImage.Source = bitmapImage; // profileImage is your Image control
                }
                catch (FormatException ex)
                {
                    MessageBox.Show($"Error converting Base64 string to byte array: {ex.Message}");
                }
            }
        }
        private void SaveTextBoxContent()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                for (int i = 0; i < textboxes.Length; i++)
                {
                    if (textboxes[i] != null)
                    {
                        // Use a consistent key format
                        string key = $"TextBox{i + 1}Content";
                        config.AppSettings.Settings[key].Value = textboxes[i].Text;
                    }
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error saving configuration.");
            }
        }


        private void LoadTextBoxContent()
        {
            try
            {
                for (int i = 0; i < textboxes.Length; i++)
                {
                    string key = $"TextBox{i + 1}Content";

                    string savedContent = ConfigurationManager.AppSettings[key];

                    if (savedContent != null)
                    {
                        textboxes[i].Text = savedContent;
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show("Error loading configuration.");
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveTextBoxContent();
            SaveImageContent();
        }

        
    }
}

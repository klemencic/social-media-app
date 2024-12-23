using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace UporabniskiVmesniki_Klemencic
{
    public static class AppSettings
    {
        public static string ImageFileName
        {
            get { return ConfigurationManager.AppSettings["ImagePath"]; }
            set { ConfigurationManager.AppSettings["ImagePath"] = value; }
        }
    }
    public class BitmapImageConverter : JsonConverter<BitmapImage>
    {
        private readonly string ImageDirectory = "Images";

        public override BitmapImage ReadJson(JsonReader reader, Type objectType, BitmapImage existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                string filePath = (string)reader.Value;
                return LoadImageFromFile(filePath);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, BitmapImage value, JsonSerializer serializer)
        {
            if (value != null)
            {
                string filePath = SaveImageToFile(value);
                writer.WriteValue(filePath);
            }
            else
            {
                writer.WriteNull();
            }
        }

        private BitmapImage LoadImageFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                BitmapImage image = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
                image.Freeze();  // Freeze the image to prevent further modifications
                return image;
            }

            return null;
        }

        private string SaveImageToFile(BitmapImage image)
        {
            if (image != null)
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImageDirectory);
                Directory.CreateDirectory(directoryPath);  // Create directory if it doesn't exist

                string filePath = Path.Combine(directoryPath, $"Image_{Guid.NewGuid()}.jpg");
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    encoder.Save(stream);
                }

                return filePath;
            }

            return null;
        }
    }
    public partial class MainWindow : Window
    {
        
        public class User : INotifyPropertyChanged
        {
            private string _ime;
            public string Ime
            {
                get { return _ime; }
                set
                {
                    if (_ime != value)
                    {
                        _ime = value;
                        OnPropertyChanged(nameof(Ime));
                    }
                }
            }
            private string _priimek;
            public string Priimek
            {
                get { return _priimek; }
                set
                {
                    if (_priimek != value)
                    {
                        _priimek = value;
                        OnPropertyChanged(nameof(Priimek));
                    }
                }
            }
            private string _status;
            public string Status
            {
                get { return _status; }
                set
                {
                    if (_status != value)
                    {
                        _status = value;
                        OnPropertyChanged(nameof(Status));
                    }
                }
            }

            public User(string ime, string priimek, string status)
            {
                Ime = ime;
                Priimek = priimek;
                Status = status;
            }
            public User() { }
            public event PropertyChangedEventHandler PropertyChanged;
           

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        [Serializable]
        public class Objava : INotifyPropertyChanged
        {
            public User Oseba { get; set; }

            private string _vsebina;

            public string Vsebina
            {
                get { return _vsebina; }
                set
                {
                    if (_vsebina != value)
                    {
                        _vsebina = value;
                        OnPropertyChanged(nameof(Vsebina));
                    }
                }
            }
            private string _tagedFriends;
            public string TagedFriends
            {
                get { return _tagedFriends; }
                set
                {
                    if (_tagedFriends != value)
                    {
                        _tagedFriends = value;
                        OnPropertyChanged(nameof(TagedFriends));
                    }
                }
            }
            public DateTime Datum { get; set; }
            private string _tema;
            public string Tema
            {
                get { return _tema; }
                set
                {
                    if (_tema != value)
                    {
                        _tema = value;
                        OnPropertyChanged(nameof(Tema));
                    }
                }
            }
            [JsonConverter(typeof(BitmapImageConverter))]
            private BitmapImage _slika;
            [JsonConverter(typeof(BitmapImageConverter))]
            public BitmapImage Slika
            {
                get { return _slika; }
                set
                {
                    if (_slika != value)
                    {
                        _slika = value;
                        OnPropertyChanged(nameof(Slika));
                    }
                }
            }

            private string _slikaFilePath;

            [JsonIgnore]
            public string SlikaFilePath
            {
                get { return _slikaFilePath; }
                set
                {
                    if (_slikaFilePath != value)
                    {
                        _slikaFilePath = value;
                        OnPropertyChanged(nameof(SlikaFilePath));
                    }
                }
            }
            public string DisplayIme => Oseba?.Ime + " " + Oseba?.Priimek;

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            public Objava(User user, string vsebina, DateTime datum, string tema, BitmapImage slika, string tagedFriends, string slikafp)
            {
                Oseba = user;
                Vsebina = vsebina;
                Datum = datum;
                Tema = tema;
                Slika = slika;
                TagedFriends = tagedFriends;
                SaveImageToFile(slika); 
                SlikaFilePath = GetImagePath();
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

            private string GetImagePath()
            {
                // Use a consistent path to save the images, e.g., in the application directory
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Image_{Guid.NewGuid()}.jpg");
            }


        }
    }
}

using LibData.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp1.Models
{
    public class UserVM : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyCahnged(nameof(Name));
                }
            }
        }
        public string Phone { get; set; }
        public string Image { get; set; }
        public string _dateCreated;

        public BitmapImage ImageFilePath
        {
            get
            {
                string image = Image=="noimage.png" ? Image: $"32_{Image}";
                string url = $"{MyAppConfig.GetSectionValue("FolderSaveImages")}/{image}";
                return toBitmap(File.ReadAllBytes(url));
            }
        }

        public static BitmapImage toBitmap(Byte[] value)
        {
            if (value != null && value is byte[])
            {
                byte[] ByteArray = value as byte[];
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(ByteArray);
                bmp.EndInit();
                return bmp;
            }
            return null;
        }

        public string DateCreated
        {
            get { return _dateCreated; }
            set
            {
                _dateCreated = value;
                NotifyPropertyCahnged(nameof(DateCreated));
            }
        }


        //public DateTime? DateUpdate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyCahnged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

}

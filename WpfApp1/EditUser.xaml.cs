using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;
using static System.Net.Mime.MediaTypeNames;
using Bogus.DataSets;
using LibData.Helpers;
using WpfApp1.Helper;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string _Image { get; set; }        
        public EditUser()
        {
            InitializeComponent();
        }
        //private void Window_ContentRendered(object sender, EventArgs e)
        private void Window_Loaded(object sender, EventArgs e)
        {
            txtUserName.Text = Name;
            txtPhone.Text = Phone;
            txtPassword.Text = Password;            
            //string userImage = Path.Combine("image", Image);
            //Foto.Source = userImage;
            if (_Image != null)
            {               

                var bitmap = new BitmapImage();

                using (var stream = new FileStream($@"images/300_{_Image}", FileMode.Open))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze(); // optional
                }
                Foto.Source = bitmap;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string dir = "images";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            

            if (_Image != null)
            {
                Bitmap bitmap = new Bitmap(_Image);
                string imageName = Path.GetRandomFileName() + ".jpg";
                string[] sizes = { "32", "100", "300", "600", "1200" };
                foreach (var size in sizes)
                {
                    int width = int.Parse(size);
                    var saveBmp = ImageWorker.CompressImage(bitmap, width, width, false);
                    saveBmp.Save($"{MyAppConfig.GetSectionValue("FolderSaveImages")}/{size}_{imageName}",
                        ImageFormat.Jpeg);
                }
                _Image = imageName;
            }

            Name = txtUserName.Text;
            Phone = txtPhone.Text;
            Password = txtPassword.Text;            
            this.Close();
        }
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (_Image != null)
            {               
                string[] sizes = { "32", "100", "300", "600", "1200" };
                foreach (var size in sizes)
                {
                    File.Delete(@$"images\{size}_{_Image}");
                }
            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == true)
            {
                _Image = dlg.FileName;
                //FileNameLabel.Content = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_Image);
                bitmap.EndInit();
                Foto.Source = bitmap;
            }
        }
    }
}

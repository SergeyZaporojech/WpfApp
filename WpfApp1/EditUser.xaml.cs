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
                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                ////var img =  System.Drawing.Image.FromFile(Path.Combine("images", _Image));
                //bitmap.UriSource = new Uri($@"images/{_Image}");
                                
                //bitmap.EndInit();
                //Foto.Source = bitmap;

                //BitmapImage bi = new BitmapImage();
                //System.Drawing.Image img;
                //MemoryStream strm = new MemoryStream();
                //strm.Write(bindata, 0, bindata.Length);
                //strm.Position = 0;
                //img = System.Drawing.Image.FromStream(strm);
                //bi.BeginInit();
                //MemoryStream ms = new MemoryStream();
                //img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //ms.Seek(0, SeekOrigin.Begin);
                //bi.StreamSource = ms;
                //bi.EndInit();
                //Foto.Source = bi; 
                //Foto.Source = BitmapFromUri(new Uri($@"images/{_Image}");

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

            Bitmap bitmap = new Bitmap(_Image);
            string imageName = Path.GetRandomFileName() + ".jpg";
            bitmap.Save(Path.Combine(dir, imageName), ImageFormat.Jpeg);

            Name = txtUserName.Text;
            Phone = txtPhone.Text;
            Password = txtPassword.Text;
            _Image = imageName;
            this.Close();
        }
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (_Image != null)
                File.Delete(@$"images\{_Image}");
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

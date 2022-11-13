using Microsoft.Win32;
using System;

using System.Collections.Generic;
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
using System.Drawing;
using System.Drawing.Imaging;
using Path = System.IO.Path;
using LibData.Helpers;
using WpfApp1.Helper;
using LibData.Entities;
using WpfApp1.Models;



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public Gender Gender { get; set; }      

        public AddUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string dir = "images";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            var index = cbGender.SelectedIndex;
            var item = (MyComboBoxItem)cbGender.Items[index];
            if (item.Id == -1)
            {
                MessageBox.Show("Вкажіть сать");
                return;
            }
            //Gender gender = (Gender)item.Id;
            //MessageBox.Show(gender.ToString());

            if (Image!=null)
            {                
                Bitmap bitmap = new Bitmap(Image);                
                string imageName = Path.GetRandomFileName() + ".jpg";
                string[] sizes = { "32", "100", "300", "600", "1200" };
                foreach (var size in sizes)
                {
                    int width = int.Parse(size);
                    var saveBmp = ImageWorker.CompressImage(bitmap, width, width, false);
                    saveBmp.Save($"{MyAppConfig.GetSectionValue("FolderSaveImages")}/{size}_{imageName}",
                        ImageFormat.Jpeg);
                }

                //bitmap.Save(Path.Combine(dir, imageName), ImageFormat.Jpeg); // формуємо файл зображення у папці та з назвою
                Image = imageName;
            }
            this.Gender = (Gender)item.Id;
            Name = txtUserName.Text;
            Phone = txtPhone.Text;
            Password = txtPassword.Text;

            this.Close();
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
  
            if (dlg.ShowDialog() == true)
            {
                Image = dlg.FileName;
                //FileNameLabel.Content = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Image);
                bitmap.EndInit();
                Foto.Source = bitmap;
            }
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            #region ComboBox
            MyComboBoxItem noData = new MyComboBoxItem
            {
                Id = -1,
                Name = "Не вказано"
            };
            cbGender.Items.Add(noData);

            MyComboBoxItem male = new MyComboBoxItem
            {
                Id = (int)Gender.Male,
                Name = "Чоловік"
            };
            cbGender.Items.Add(male);
            MyComboBoxItem female = new MyComboBoxItem
            {
                Id = (int)Gender.Female,
                Name = "Жінка"
            };
            cbGender.Items.Add(female);

            cbGender.SelectedIndex = 0;

            #endregion
        }


    }
}

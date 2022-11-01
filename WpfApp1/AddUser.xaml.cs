using Microsoft.Win32;
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
        public AddUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
            //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    string selectedFileName = dlg.FileName;
            //    //FileNameLabel.Content = selectedFileName;
            //    BitmapImage bitmap = new BitmapImage();
            //    bitmap.BeginInit();
            //    bitmap.UriSource = new Uri(selectedFileName);
            //    bitmap.EndInit();
            //    Foto.Source = bitmap;
            //}
        }
    }
}

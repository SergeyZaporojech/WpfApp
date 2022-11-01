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
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for RegistrWindow.xaml
    /// </summary>
    public partial class RegistrWindow : Window
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

        public RegistrWindow()
        {
            InitializeComponent();
            //Foto.Source = new BitmapImage(new Uri(""));
            
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Name = txtUserName.Text;
            Phone = txtPhone.Text;
            Password = txtPassword.Text;
            this.Close();
        }
    }
}

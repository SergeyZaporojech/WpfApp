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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
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

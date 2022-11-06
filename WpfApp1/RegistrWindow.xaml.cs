using LibData.Entities;
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
using WpfApp1.Models;
using static Bogus.DataSets.Name;
using static System.Net.Mime.MediaTypeNames;
using Gender = LibData.Entities.Gender;

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


        private void Registring_Click(object sender, RoutedEventArgs e)
        {
            //Name = txtUserName.Text;
            //Phone = txtPhone.Text;
            //Password = txtPassword.Text;

            var index = cbGender.SelectedIndex;
            var item = (MyComboBoxItem)cbGender.Items[index];
            if (item.Id == -1)
            {
                MessageBox.Show("Вкажіть сать");
                return;
            }
            Gender gender = (Gender)item.Id;
            MessageBox.Show(gender.ToString());
            //if (lbItem.SelectedItem != null)
            //{
            //    var lb = (MyComboBoxItem)lbItem.SelectedItem;
            //    MessageBox.Show(lb.Name);
            //} 
            if (lbItem.SelectedItems.Count>0)
            {
                string str = "";
                foreach (var data in lbItem.SelectedItems)
                {
                    var lb = (MyComboBoxItem)data;
                    str += $"{lb.Name}  ";
                }
                MessageBox.Show(str);
            }
             
            //this.Close();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
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

            #region ListBox

            string[] professin = { "Менеджер", "Програміст", "Економіст", "Охоронець" };
            int i = 1;
            foreach (var item in professin)
            {
                MyComboBoxItem data = new MyComboBoxItem
                {
                    Id = i++,
                    Name = item
                };
                lbItem.Items.Add(data);
            }

            #endregion
        }
    }
}

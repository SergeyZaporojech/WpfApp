﻿using Npgsql.Internal.TypeHandling;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public LoginWindow()
        {
            InitializeComponent();

        }

        private void ___btnLogin__Click(object sender, RoutedEventArgs e)
        {
            Name = txtUserName.Text;
            Password = txtPassvord.Text;
            this.Close();
        }
    }
}

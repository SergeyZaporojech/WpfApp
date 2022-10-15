using LibData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void mFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void mActionREgister_Click(object sender, RoutedEventArgs e)
        {
            RegistrWindow window = new RegistrWindow();
            window.ShowDialog();
        }

 
        private void mActionLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.ShowDialog();
        }

        private void btnConnetion_Click(object sender, RoutedEventArgs e)
        {
            using (MyDataContext context = new MyDataContext())
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                btnConnetion.Content += " - " + context.Users.Count().ToString();
                //MessageBox.Show(context.Users.Count().ToString());

                // Get the elapsed time as a TimeSpan value.
                TimeSpan ts = stopWatch.Elapsed;

                // Format and display the TimeSpan value.
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                  ts.Hours, ts.Minutes, ts.Seconds,
                  ts.Milliseconds / 10);
                MessageBox.Show("RunTime " + elapsedTime);
            }
        }
    }
}

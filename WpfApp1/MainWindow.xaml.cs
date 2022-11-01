using Bogus;
using LibData;
using LibData.Delegates;
using LibData.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event ConnectionCompleteDelegate _connectionComplete;
        private MyDataContext _myDataContext;
        private CancellationTokenSource ctSourse;
        private CancellationToken token;
        private static ManualResetEvent _mre = new ManualResetEvent(false);
        private bool _isPause = false;

        class MyImage
        {
            public string Name { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            _connectionComplete += MainWindow__connectionComplete;
            Thread thread = new Thread(ConnectionDatabase);
            thread.Start();

            var faker = new Faker<MyImage>()
            .RuleFor(u => u.Name, f => f.Image.LoremFlickrUrl());
            var img = faker.Generate();
        }
        private void ConnectionDatabase()
        {
            MyDataContext myDataContext = new MyDataContext();
            myDataContext.ChangeTracker.AutoDetectChangesEnabled = false;        //відключаємо трекер для додавання швидкості додавання
            _connectionComplete?.Invoke(myDataContext);
        }

        private void MainWindow__connectionComplete(MyDataContext context)
        {
            _myDataContext = context;
            Dispatcher.Invoke(() =>
            {
                lblStatusBar.Content = "Connection successfully completed";
            });
        }

        private void mFileExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void mActionREgister_Click(object sender, RoutedEventArgs e)
        {
            RegistrWindow window = new RegistrWindow();
            window.ShowDialog();
            UserEntitie newUser = new UserEntitie
            {
                Name = window.Name,
                Phone = window.Phone,
                Password = window.Password,
                DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
            };
            _myDataContext.Users.Add(newUser);
            _myDataContext.SaveChanges();
        }

 
        private void mActionLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.ShowDialog();
            var users = _myDataContext.Users
                .AsQueryable();
            bool isLogin = false;
            foreach (var item in users)
            {
                if (item.Name == window.Name && item.Password == window.Password)
                {
                    window.Close();
                    isLogin = true;
                }                  
            }
            if (isLogin)
            {
                UsersWindows userWindow = new UsersWindows(_myDataContext);
                userWindow.Show();
            }
            else MessageBox.Show($"Ім'я користувача чи  пароль невірні");
        }

        private void btnAddUsers_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(txtCount.Text);
            pbCount.Minimum = 0;
            pbCount.Maximum = count;
            ctSourse= new CancellationTokenSource();
            token = ctSourse.Token;

            Task thread = new Task(()=>AddUsers(count),token);
            thread.Start();
            _mre.Set();           
        }
        private void AddUsers(object count)
        {
            int countAdd = (int)(count);
                var testUser = new Faker<UserEntitie>("uk")
                    .RuleFor(o => o.Name, f => f.Name.FullName())
                    .RuleFor(o => o.Phone, f => f.Person.Phone)
                    .RuleFor(o => o.Password, f => f.Internet.Password())
                    .RuleFor(o => o.DateCreated, f => DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc));

            for (int i = 0; i < countAdd; i++)
            {
                var user = testUser.Generate();
                _myDataContext.Users.Add(user);

               //_myDataContext.Users.Add(new UserEntitie
               // {
               //     Name = "Іван",
               //     Phone = "097 56 56 765",
               //     Password = "123456",
               //     DateCreated = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Utc),
               // });
                _myDataContext.SaveChanges();
                Dispatcher.Invoke(() =>
                {
                    pbCount.Value=i;
                    lblStatusBar.Content = $"{i+1}/{count}";
                });

                _mre.WaitOne(Timeout.Infinite);


                if (token.IsCancellationRequested)
                {
                    Dispatcher.Invoke(() =>
                    {
                        pbCount.Value = 0;
                        lblStatusBar.Content += $"  Add cansel";
                    });
                    return;
                }
            }
        }

        private void bntCansel_Click(object sender, RoutedEventArgs e)
        {
            ctSourse.Cancel();
        }

        private void bntPause_Click(object sender, RoutedEventArgs e)
        {
            if (_isPause)   //якщо потік був задлчений
            {
                _mre.Set();
                bntPause.Content = "Pause";
            }
            else
            {
                _mre.Reset();// лочимо потік
                bntPause.Content = "Restore";
            }
            _isPause = !_isPause;
        }

        private void mActionUsrers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindows userWindow = new UsersWindows(_myDataContext);
            userWindow.Show();
        }
    }
}

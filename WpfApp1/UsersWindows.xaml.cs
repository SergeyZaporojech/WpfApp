using LibData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UsersWindows.xaml
    /// </summary>
    public partial class UsersWindows : Window
    {
        private ObservableCollection<UserVM> users = new ObservableCollection<UserVM>();
        private readonly MyDataContext _myDataContext;
        public UsersWindows(MyDataContext myDataContext)            
        {
            _myDataContext = myDataContext;
            InitializeComponent();
            InitDataGrid();
        }

        private void InitDataGrid()
        {
            var cultureInfo = new CultureInfo("uk-UA");
            var users = _myDataContext.Users
                .OrderBy(x => x.Id)
                .Select(x => new UserVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    DateCreated = x.DateCreated != null ?
                    x.DateCreated.Value.ToString("dd MMMM yyyy HH:mm:ss", cultureInfo) :""
                }).ToList();

            //users.Add(new UserVM
            //{
            //    Name ="Іван Петров",
            //    Phone = "+380974345 56 45"
            //});
            dgUsers.ItemsSource = users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new UserVM
            {
            Name= "Матрос",
            Phone = "097 768 34 56"
            });
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem != null)
                if (dgUsers.SelectedItem is UserVM)
                {
                    var userVM = (UserVM)dgUsers.SelectedItem;
                    var user = _myDataContext.Users.SingleOrDefault(x => x.Id == userVM.Id);
                    if (user != null)
                    {
                        user.Name = "Оновили користувача :)";
                        _myDataContext.Users.Update(user);
                        _myDataContext.SaveChanges();
                        userVM.Name = user.Name;

                    }
                   // userVM.Name = "Оновили користувача :)";
                }
        }
    }
}

using LibData;
using LibData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
        private readonly MyDataContext _myDataContext;                                                  //строка підключення 
        int? page;
       const int PageSize = 5;
        int totalCount = 0;
        int totalPage = 0;

        public UsersWindows(MyDataContext myDataContext)            
        {
            _myDataContext = myDataContext;
            InitializeComponent();
           // InitDataGrid();
        }

        private async Task  InitDataGrid(IQueryable<UserEntitie> query)                                //заповнення дата грид даними з БД
        {                                                                                     
            var cultureInfo = new CultureInfo("uk-UA");                                                //визначаємо регіональні параметри 
            int threadId = Thread.CurrentThread.ManagedThreadId;

            Stopwatch stopWatch = new Stopwatch();                                                     // створюємо для обрахунку часу виконання дії
            stopWatch.Start();
            totalCount = query.Count();
            totalPage = (int)Math.Ceiling(totalCount / (double)PageSize);
            int skip = (page ?? 0) * PageSize;
            var users = await query//_myDataContext.Users                                              //формуємо ліст юзерів відсортований по айд 
                .OrderBy(x => x.Id)
                .Select(x => new UserVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    DateCreated = x.DateCreated != null ?
                    x.DateCreated.Value.ToString("dd MMMM yyyy HH:mm:ss", cultureInfo) :"",
                    Image = x.Image??"noimage.png"
                })
                    .Skip(skip)
                    .Take(PageSize)
                    .ToListAsync();

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            //MessageBox.Show("RunTime " + elapsedTime);
            labeTime.Content = $"RunTime {elapsedTime}";
            labelInfo.Content = $"{skip} - {skip+PageSize} /{totalCount}";

            threadId = Thread.CurrentThread.ManagedThreadId;

            //users.Add(new UserVM
            //{
            //    Name ="Іван Петров",
            //    Phone = "+380974345 56 45"
            //});
            dgUsers.ItemsSource = users;                                                      //додаємо данні до дата грід
        }                                                                                    
                                                                                             
        private void btnAddUser_Click(object sender, RoutedEventArgs e)                       //додавання юзера одного
        {
            AddUser window = new AddUser();
            window.ShowDialog();
            UserEntitie newUser = new UserEntitie
            {
                Name = window.Name,
                Phone = window.Phone,
                Password = window.Password,
                DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
                //додати дату створення юзера
            };
           
            _myDataContext.Users.Add(newUser);
            _myDataContext.SaveChanges();
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)                     //коригування ім'я вибраного юзера
        {
            if (dgUsers.SelectedItem != null)
                if (dgUsers.SelectedItem is UserVM)
                {
                    var userVM = (UserVM)dgUsers.SelectedItem;                               //приводемо до UserVM данних вибраного рядка
                    var user = _myDataContext.Users.SingleOrDefault(x => x.Id == userVM.Id); //доступаємося до даних юзера який співпадає по ID 
                    if (user != null)
                    {
                        EditUser editUser = new EditUser 
                            { Name = user.Name,
                            Phone=user.Phone,
                            Password=user.Password
                        };
                        //editUser.Show();

                        if (!editUser.ShowDialog() == editUser.ShowActivated) 
                        {
                            UserEntitie updateUser = new UserEntitie
                            {                                
                                Name = editUser.Name,
                                Phone = editUser.Phone,
                                Password = editUser.Password,
                                DateUbdate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
                            };
                            _myDataContext.Users.Update(updateUser);
                            _myDataContext.SaveChanges();
                            userVM.Name = editUser.Name;
                            userVM.Phone = editUser.Phone;
                        }
                    }                   
                }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            
            InitDataGrid(ReadDataSearch());
        }
        private IQueryable<UserEntitie> ReadDataSearch()
        {
            var query = _myDataContext.Users.AsQueryable();
            SearchUser search = new SearchUser();
            search.Name = txtName.Text;
            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }
            int count = query.Count();
            return query;
        }

        private void btnPev_Click(object sender, RoutedEventArgs e)
        {
            int p = (page ?? 0);
            if (p ==0) return;
            page = --p;
            InitDataGrid(ReadDataSearch());           
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            int p = (page ?? 0);
            if ((page ?? 0) >= totalPage) return;
            page = ++p;
            var query = ReadDataSearch();
            InitDataGrid(query);
        }
    }
}

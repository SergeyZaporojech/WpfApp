using LibData.Entities;
using LibData.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibData
{
    public class MyDataContext : DbContext                                             //наслідуємся від DbContext 
    {
        public MyDataContext()
        {
            
            this.Database.Migrate();                                                   //для міграції при роботі з декількома БД
        }
        public DbSet<UserEntitie> Users { get; set; }                                  //таблиця юзерів

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  //оверрайд метода OnConfiguring
        {
                                            
            var dbConnection = MyAppConfig.GetSectionValue("ConnectionBD");

            optionsBuilder.UseNpgsql(dbConnection);

        //    optionsBuilder.UseNpgsql("" +                                          //колишня строка підключення до БД
        //        "Server=localhost;" +
        //        "Port=5432;" +
        //        "Database=wpfdata;" +
        //        "User Id=postgres;" +
        //        "Password=123456;");
        }
    }
}

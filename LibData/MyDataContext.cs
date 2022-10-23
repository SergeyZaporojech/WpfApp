using LibData.Entities;
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
    public class MyDataContext : DbContext
    {
        public MyDataContext()
        {
            this.Database.Migrate();
        }
        public DbSet<UserEntitie> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");
            var config = configBuilder.Build();
            var dbConnection = config.GetSection("ConnectionBD").Value;

            optionsBuilder.UseNpgsql(dbConnection);

        //    optionsBuilder.UseNpgsql("" +
        //        "Server=localhost;" +
        //        "Port=5432;" +
        //        "Database=wpfdata;" +
        //        "User Id=postgres;" +
        //        "Password=123456;");
        }
    }
}

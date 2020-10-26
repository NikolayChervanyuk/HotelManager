using Data.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class HmDbContext : DbContext
    {
        //By default, connectionString takes the connection string from App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["HmDbContext"].ConnectionString;
        public HmDbContext() {this.Database.Connection.ConnectionString = connectionString;}
        public HmDbContext(string connectionString)
        {
            this.connectionString = connectionString;
            this.Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}

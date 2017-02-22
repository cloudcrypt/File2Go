using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F2G;

namespace F2GClient
{
    class F2GContext : DbContext
    {
        private static string usr = "f2gadmin";
        private static string pswrd = "f2g2017!";
        private static string connStr = $@"Server=tcp:f2g.database.windows.net,1433;
                                        Initial Catalog = File2Go; 
                                        Persist Security Info=False;
                                        User ID ={usr}; 
                                        Password={pswrd};
                                        MultipleActiveResultSets=False;
                                        Encrypt=True;
                                        TrustServerCertificate=False;
                                        Connection Timeout = 30;";

        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.email);
        }
    }
}

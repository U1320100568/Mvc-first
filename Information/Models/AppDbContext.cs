using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Information.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Infor> Informations { get; set; }
        public DbSet<LogRecord> LogRecords { get; set; }
        public DbSet<Feature> Features { get; set; }    

        public AppDbContext()  :  base("DefaultConnection")
        {

        }

        
    }
}
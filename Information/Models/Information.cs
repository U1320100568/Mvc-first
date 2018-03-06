using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Information.Models
{
    public class Infor
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string publisher { get; set; }

        [Display(Name ="Release Time")]
        [DataType(DataType.Date)]
        public DateTime ReleaseTime { get; set; }
        public string content { get; set; }
    }

    public class InforDbContext : DbContext
    {
        public DbSet<Infor> Informations { get; set; }
    }
}
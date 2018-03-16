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
        [Key]
        public int ID { get; set; }

        [StringLength(200, MinimumLength =3)]
        [Required]
        public string Title { get; set; }

        [StringLength(20)]
        public string Publisher { get; set; }

        [Display(Name = "Release Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime ReleaseTime { get; set; }

        [Required]
        [StringLength(300)]
        public string Content { get; set; }
    }
    /*
    public class InforDbContext : DbContext
    {
        public DbSet<Infor> Informations { get; set; }
    }
    */
}
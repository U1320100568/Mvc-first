using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Information.Service;

namespace Information.Models
{
    public class Member 
    {
        [Key]
        public int ID { get; set; }
        //public int PowerID { get; set; }

        

        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        [Required]
        [StringLength(12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    
    
    
}
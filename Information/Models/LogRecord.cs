using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Information.Helpers;

namespace Information.Models
{
    public class LogRecord
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "會員")]
       
        public int MemberId { get; set; }
        
        public DateTime LoginTime { get; set; }

        public DateTime LogoutTime { get; set; }

    }
}
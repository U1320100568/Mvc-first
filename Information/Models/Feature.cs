using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Information.Models
{
    public class Feature
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "會員")]
        public int MemberId { get; set; }
        [Display(Name = "公告功能")]
        public bool FeatInfor { get; set; }
        [Display(Name = "登入紀錄功能")]
        public bool FeatLogRec { get; set; }


        public string GetFirstAccessFeature()
        {
            if(FeatInfor)
            {
                return "Infors";
            }
            if(FeatLogRec)
            {
                return "LogRecords";
            }
            return null;
        }
    }

   
}
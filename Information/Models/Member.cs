using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Information.Models
{
    public class Member
    {
        public int ID { get; set; }
        public int PowerID { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }


    }
    public static class User
    {
        public static int UserID{ get; set; }
    }

    public class MemberDbContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
    }
}
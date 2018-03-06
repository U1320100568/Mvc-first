namespace Information.Migrations
{
    using Information.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Information.Models.MemberDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Information.Models.MemberDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Members.AddOrUpdate(i => i.Name,
                    new Member
                    {
                        Name = "admit",
                        PowerID = 0,
                        Password="123"
                    },
                    new Member
                    {
                        Name = "menber",
                        PowerID = 1,
                        Password = "123"
                    }
                );
        }
    }
}

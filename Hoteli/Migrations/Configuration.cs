namespace Hoteli.Migrations
{
    using Hoteli.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Hoteli.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Hoteli.Models.ApplicationDbContext context)
        {
            context.LanacHotelas.AddOrUpdate(x => x.Id,
               new LanacHotela() { Id = 1, Naziv = "Hilton Worldwide", GodinaOsnivanja = 1919 },
               new LanacHotela() { Id = 2, Naziv = "Mariot International", GodinaOsnivanja = 1927 },
               new LanacHotela() { Id = 3, Naziv = "Kempinski", GodinaOsnivanja = 1897 }
               );
            context.SaveChanges();

            context.Hotels.AddOrUpdate(x => x.Id,
               new Hotel() { Id = 1, Naziv = "Sheraton Novi Sad", GodinaOsnivanja = 2018, BrojZaposlenih = 70, BrojSoba = 150 , LanacHotelaId = 2 },
               new Hotel() { Id = 2, Naziv = " Hilton Belgrade", GodinaOsnivanja = 2017, BrojZaposlenih = 100, BrojSoba = 242, LanacHotelaId = 1 },
               new Hotel() { Id = 3, Naziv = "Palais Hansen", GodinaOsnivanja = 2013, BrojZaposlenih = 80, BrojSoba = 152, LanacHotelaId = 3 },
               new Hotel() { Id = 4, Naziv = "Budapest Marriott", GodinaOsnivanja = 1994, BrojZaposlenih = 130, BrojSoba = 364, LanacHotelaId = 2 },
               new Hotel() { Id = 5, Naziv = "Hilton Berlin", GodinaOsnivanja = 1991, BrojZaposlenih = 200, BrojSoba = 601, LanacHotelaId = 1 }
               );
            context.SaveChanges();
        }
    }
}

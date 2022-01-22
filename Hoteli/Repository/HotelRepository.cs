using Hoteli.Interfaces;
using Hoteli.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Hoteli.Repository
{
    public class HotelRepository : IDisposable, IHotelRepository
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        public IQueryable<Hotel> GetAll()
        {
            return db.Hotels.OrderBy(x => x.GodinaOsnivanja);
        }


        public Hotel GetById(int id)
        {
            return db.Hotels.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Hotel hotel)
        {
            db.Hotels.Add(hotel);
            db.SaveChanges();
        }

        public void Update(Hotel hotel)
        {
            db.Entry(hotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Hotel hotel)
        {
            db.Hotels.Remove(hotel);
            db.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<LanacHotelaDTO> GetZaposleni()
        {
            IEnumerable<Hotel> hoteli = GetAll().ToList();
            var rezultat = hoteli.GroupBy(
            g => g.LanacHotela,
            g => g.BrojZaposlenih,
            (lanac, brojZaposlenih) => new LanacHotelaDTO()
            {
                Id = lanac.Id,
                Naziv = lanac.Naziv,
                BrojZaposlenih = brojZaposlenih.Sum()
            }).OrderByDescending(r => r.BrojZaposlenih);
            return rezultat.AsEnumerable();
        }

        public IEnumerable<LanacHotelaDTO> PostSobe(int minSoba)
        {
            IEnumerable<Hotel> hoteli = GetAll().ToList();
            var rezultat = hoteli.GroupBy(
            g => g.LanacHotela,
            g => g.BrojSoba,
            (lanac, brojSoba) => new LanacHotelaDTO()
            {
                Id = lanac.Id,
                Naziv = lanac.Naziv,
                BrojSoba = brojSoba.Sum()
            }).Where(x => x.BrojSoba >= minSoba).OrderBy(r => r.BrojSoba);
            return rezultat.AsEnumerable();
        }

        public IQueryable<Hotel> GetZaposleni(int minZaposleni)
        {
            return db.Hotels.Where(x => x.BrojZaposlenih >= minZaposleni).OrderBy(x => x.BrojZaposlenih);
        }

        public IQueryable<Hotel> PostKapacitet(PretragaDTO pretraga)
        {
            return db.Hotels.Where(x => x.BrojSoba >= pretraga.MinSoba && x.BrojSoba <= pretraga.MaxSoba).OrderByDescending(x => x.BrojSoba);
        }
    }
}
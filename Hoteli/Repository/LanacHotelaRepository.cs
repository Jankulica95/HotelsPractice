using Hoteli.Interfaces;
using Hoteli.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Hoteli.Repository
{
    public class LanacHotelaRepository : IDisposable, ILanacHotelaRepository
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        public IQueryable<LanacHotela> GetAll()
        {
            return db.LanacHotelas;
        }


        public LanacHotela GetById(int id)
        {
            return db.LanacHotelas.FirstOrDefault(p => p.Id == id);
        }

        public void Add(LanacHotela lanacHotela)
        {
            db.LanacHotelas.Add(lanacHotela);
            db.SaveChanges();
        }

        public void Update(LanacHotela lanacHotela)
        {
            db.Entry(lanacHotela).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(LanacHotela lanacHotela)
        {
            db.LanacHotelas.Remove(lanacHotela);
            db.SaveChanges();
        }

        public IQueryable<LanacHotela> GetTradicija()
        {
            return db.LanacHotelas.OrderBy(x => x.GodinaOsnivanja).Take(2);
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
    }
}
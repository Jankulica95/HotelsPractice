using Hoteli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoteli.Interfaces
{
    public interface IHotelRepository
    {
        IQueryable<Hotel> GetAll();
        Hotel GetById(int id);
        void Add(Hotel hotel);
        void Update(Hotel hotel);
        void Delete(Hotel hotel);
        IEnumerable<LanacHotelaDTO> GetZaposleni();
        IEnumerable<LanacHotelaDTO> PostSobe(int minSoba);
        IQueryable<Hotel> GetZaposleni(int minZaposleni);
        IQueryable<Hotel> PostKapacitet(PretragaDTO pretraga);
    }
}

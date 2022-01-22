using Hoteli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoteli.Interfaces
{
    public interface ILanacHotelaRepository
    {
        IQueryable<LanacHotela> GetAll();
        LanacHotela GetById(int id);
        void Add(LanacHotela lanacHotela);
        void Update(LanacHotela lanacHotela);
        void Delete(LanacHotela lanacHotela);
        IQueryable<LanacHotela> GetTradicija();
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hoteli.Models
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int GodinaOsnivanja { get; set; }
        public int BrojZaposlenih { get; set; }
        public int BrojSoba { get; set; }
        public string LanacHotelaNaziv { get; set; }
        public int LanacHotelaId { get; set; }
    }
}
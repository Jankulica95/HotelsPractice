using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hoteli.Models
{
    public class LanacHotelaDTO
    {
        public int Id { get; set; }
        public string Naziv  { get; set; }
        public int GodinaOsnivanja { get; set; }
        public int BrojZaposlenih { get; set; }
        public int BrojSoba { get; set; }
    }
}
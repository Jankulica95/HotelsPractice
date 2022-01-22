using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hoteli.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv hotela je obavezan.")]
        [StringLength(80, ErrorMessage = "Dozvoljeno je max 80 karaktera")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Godina otvaranja je obavezna.")]
        [Range(1950, 2020)]
        public int GodinaOsnivanja { get; set; }

        [Required(ErrorMessage = "Broj zaposlenih je obavezan.")]
        [Range(1, int.MaxValue)]
        public int BrojZaposlenih { get; set; }

        [Required(ErrorMessage = "Broj soba je obavezan.")]
        [Range(10, 999)]
        public int BrojSoba { get; set; }



        public virtual LanacHotela LanacHotela { get; set; }
        public virtual int LanacHotelaId { get; set; }
    }
}
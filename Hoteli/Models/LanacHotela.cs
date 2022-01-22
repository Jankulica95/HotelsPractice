using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hoteli.Models
{
    public class LanacHotela
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Naziv hotela je obavezan.")]
        [StringLength(75, ErrorMessage = "Dozvoljeno je max 75 karaktera")]
        public string Naziv { get; set; }


        [Required(ErrorMessage = "Godina osnivanja je obavezna.")]
        [Range(1950, 2020)]
        public int GodinaOsnivanja { get; set; }
    }
}
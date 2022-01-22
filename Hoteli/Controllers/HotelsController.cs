using Hoteli.Interfaces;
using Hoteli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hoteli.Controllers
{
    public class HotelsController : ApiController
    {
        IHotelRepository _repository { get; set; }

        public HotelsController(IHotelRepository repository)
        {
            _repository = repository;
        }


        public IQueryable<HotelDTO> Get()
        {
            return _repository.GetAll().ProjectTo<HotelDTO>();
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var hotel = _repository.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<HotelDTO>(hotel));
        }

        public IHttpActionResult Post(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(hotel);
            return CreatedAtRoute("DefaultApi", new { id = hotel.Id }, hotel);
        }

        public IHttpActionResult Put(int id, Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(hotel);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(hotel);
        }

        public IHttpActionResult Delete(int id)
        {
            var hotel = _repository.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _repository.Delete(hotel);
            return Ok();
        }

            
        [Route("api/zaposleni")]
        public IEnumerable<LanacHotelaDTO> GetZaposleni()
        {
            return _repository.GetZaposleni();
        }

        [Route("api/sobe")]
        public IEnumerable<LanacHotelaDTO> PostSobe(int minSobe)
        {
            return _repository.PostSobe(minSobe);
        }

        [Route("api/hoteli")]
        public IEnumerable<Hotel> GetZaposleniMin(int zaposleni)
        {
            return _repository.GetZaposleni(zaposleni);
        }

        [Route("api/kapacitet")]
        public IQueryable<HotelDTO> PostKapacitet(PretragaDTO pretraga)
        {
            return _repository.PostKapacitet(pretraga).ProjectTo<HotelDTO>();
        }

    }
}


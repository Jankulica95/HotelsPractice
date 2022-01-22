using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hoteli.Interfaces;
using Hoteli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hoteli.Controllers
{
    public class LanacHotelasController : ApiController
    {
        ILanacHotelaRepository _repository { get; set; }

        public LanacHotelasController(ILanacHotelaRepository repository)
        {
            _repository = repository;
        }


        public IQueryable<LanacHotelaDTO> Get()
        {
            return _repository.GetAll().ProjectTo<LanacHotelaDTO>();
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var lanacHotela = _repository.GetById(id);
            if (lanacHotela == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<LanacHotelaDTO>(lanacHotela));
        }

        public IHttpActionResult Post(LanacHotela lanacHotela)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(lanacHotela);
            return CreatedAtRoute("DefaultApi", new { id = lanacHotela.Id }, lanacHotela);
        }

        public IHttpActionResult Put(int id, LanacHotela lanacHotela)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lanacHotela.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(lanacHotela);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(lanacHotela);
        }

        public IHttpActionResult Delete(int id)
        {
            var lanacHotela = _repository.GetById(id);
            if (lanacHotela == null)
            {
                return NotFound();
            }

            _repository.Delete(lanacHotela);
            return Ok();
        }

        [Route("api/tradicija")]
        public IQueryable<LanacHotelaDTO> GetTradicija()
        {
            return _repository.GetTradicija().ProjectTo<LanacHotelaDTO>();
        }
    }
}

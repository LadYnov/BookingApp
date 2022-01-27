using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcertController : ControllerBase
    {
        private IConcertRepository _concertRepository;

        public ConcertController(IConcertRepository concertRepository)
        {
            this._concertRepository = concertRepository;
        }

        // GET: api/<ConcertController>
        [HttpGet]
        public async Task<IEnumerable<Concert>> Get()
        {
            return await _concertRepository.GetConcerts();
        }

        // GET api/<ConcertController>/5
        [HttpGet("{id}")]
        public async Task<Concert> Get(int id)
        {
            //return Concerts.FirstOrDefault( c => c.Id == id); // FirstOrDefault boucle (avec la fonction lambda en parametre) si l'Id de Concert est égal à id (parametre de Get()) alors retourne le Concert selon l'Id

            return await _concertRepository.GetConcertById(id);
        }

        // POST api/<ConcertController>
        [HttpPost]
        public IActionResult Post([FromBody] Concert concert)
        {
            //Concerts.Add(concert);

            _concertRepository.AddConcert(concert);

            return Created($"http://localhost:5000/api/Concerts/{concert.Id}", concert);
        }

        // PUT api/<ConcertController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Concert concert)
        {
            /*var oldConcert = Concerts.FirstOrDefault(c => c.Id == id);
            Concerts.Remove(oldConcert);

            Concerts.Add(concert);*/

            _concertRepository.UpdateConcert(id, concert);
        }

        // DELETE api/<ConcertController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            /*var deleteConcert = Concerts.FirstOrDefault(c => c.Id == id);

            if (deleteConcert == null)
            {
                return NotFound();
            }

            Concerts.Remove(deleteConcert);

            return this.NoContent();*/

            var deleteConcert = await _concertRepository.GetConcertById(id);

            if (deleteConcert == null)
            {
                return NotFound();
            }

            _concertRepository.DeleteConcert(deleteConcert);

            return NoContent();
        }
    }
}

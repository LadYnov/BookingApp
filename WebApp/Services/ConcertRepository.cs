using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public class ConcertRepository : IConcertRepository
    {
        
        private static readonly List<Concert> Concerts = new()
        {
            new Concert
            {
                Id = 1, ConcertHall = "Bercy",
                Artist = "Orelsan",
                Tickets = CreateTicket(1)
                
            },
            new Concert
            {
                Id = 2, ConcertHall = "Arena",
                Artist = "Dinos",
                Tickets = CreateTicket(2)
            }
        };

        public async Task<Concert> AddConcert(Concert concert)
        {
            Concerts.Add(concert);

            return await Task.FromResult(concert);
        }

        private static List<Ticket> CreateTicket(int idConcert)
        {
            var tickets = new List<Ticket>();
            for (int i = 1; i <= 150; i++)
            {
                tickets.Add(new Ticket
                {
                    Id = i,
                    IdConcert = idConcert
                });
            }

            return tickets;
        }

        public void DeleteConcert(Concert concert)
        {
            Concerts.Remove(concert);
        }
        


        public async Task<Ticket> ReserveTicket(Ticket ticket, Concert concert)
        {
            
            concert.Tickets.Remove(ticket);
            var oldConcert = Concerts.FirstOrDefault(c => c.Id == concert.Id);
            Concerts.Remove(oldConcert);
            Concerts.Add(concert);
            return await Task.FromResult(ticket);
        }

        public async Task<Ticket> GetTicketByConcertId(Concert concert)
        {
            return await Task.FromResult(concert.Tickets.FirstOrDefault());
        }

        public async Task<Concert> GetConcertById(int id)
        {
            return await Task.FromResult(Concerts.FirstOrDefault(c => c.Id == id));
        }

        public async Task<IEnumerable<Concert>> GetConcerts()
        {
            return await Task.FromResult(Concerts);
        }

        public async Task<Concert> UpdateConcert(int id, Concert concert)
        {
            var oldConcert = Concerts.FirstOrDefault(c => c.Id == id);
            Concerts.Remove(oldConcert);
            Concerts.Add(concert);
            return await Task.FromResult(oldConcert);
        }
    }
}

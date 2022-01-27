using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public interface IConcertRepository
    {
        Task<IEnumerable<Concert>> GetConcerts();
        
        Task<Concert> GetConcertById(int id);

        Task<Concert> AddConcert(Concert concert);

        Task<Concert> UpdateConcert(int id, Concert concert);

        void DeleteConcert(Concert concert);
        Task<Ticket> ReserveTicket(Ticket ticket, Concert concert);

        Task<Ticket> GetTicketByConcertId(Concert concert);
    }
}

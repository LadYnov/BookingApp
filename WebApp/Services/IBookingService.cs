using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Services
{
    public interface IBookingService
    {
        public Task<IEnumerable<Concert>> GetConcertsInformationToReserveAPlaceAsync();
        public Task<Concert> GetConcertsInformationToReserveById(int id);
        public Task<Ticket> ReserveAPlace(Concert concert);

        public Task<Ticket> GetTicketToReserveByConcertId(Concert concert);

    }
}

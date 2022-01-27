using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp;

namespace ClientWS;

    public interface IBookingService
    {
        public Task<IEnumerable<Concert>> GetConcertsInformationToReserveAPlaceAsync();
        public Task<Concert> GetConcertsInformationToReserveById(int id);
        public Task<Ticket> ReserveAPlace(Concert concert);
    }


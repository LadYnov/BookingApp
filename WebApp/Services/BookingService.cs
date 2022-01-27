using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using DomainShared.Exceptions.TicketException;
using RabbitMQ.Client;
using IConnectionFactory = Microsoft.AspNetCore.Connections.IConnectionFactory;

namespace WebApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IConcertRepository _concertRepository;
        private readonly IMailingService _mailingService;
        private readonly IPaymentService _paymentService;
        

        public BookingService(IConcertRepository concertRepository, IMailingService mailingService, IPaymentService paymentService)
        {
            _concertRepository = concertRepository;
            _mailingService = mailingService;
            _paymentService = paymentService;
        }

        public async Task<IEnumerable<Concert>> GetConcertsInformationToReserveAPlaceAsync()
        {
            return await _concertRepository.GetConcerts();
        }

        public async Task<Ticket> ReserveAPlace(Concert concertOfTicket)
        {
            
            var ticket = await GetTicketToReserveByConcertId(concertOfTicket);
            if (ticket is null)
            {
                throw new NoMoreTicket();
            }
            
            var successedPayment = await _paymentService.SendPaymentInformation();
            if (!successedPayment)
            {
                throw new PaymentException();
            }

            await _concertRepository.ReserveTicket(ticket, concertOfTicket);
            var concertDto = new ConcertDto()
            {
                Id = concertOfTicket.Id,
                Artist = concertOfTicket.Artist,
                ConcertHall = concertOfTicket.ConcertHall,
                Ticket = ticket
            };
            _mailingService.SendingMessageQueue($"{JsonSerializer.Serialize(concertDto)}");
            return ticket;

        }

        public async Task<Concert> GetConcertsInformationToReserveById(int id)
        {
            return await _concertRepository.GetConcertById(id);
        }

        public async Task<Ticket> GetTicketToReserveByConcertId(Concert concert)
        {
            return await _concertRepository.GetTicketByConcertId(concert);
        }
    }
}

using ClientWS;
using StreamJsonRpc;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApp;

namespace ClientWS;

    public class Program
    { 
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new Uri("wss://localhost:5001/api/websocket"), CancellationToken.None);
            var messageHandler = new WebSocketMessageHandler(webSocket);
            var jsonRpc = new JsonRpc(messageHandler, new BookingNotifications());
            
            jsonRpc.StartListening();
            var bookingService = jsonRpc.Attach<IBookingService>();
            while(true)
            {
                Console.WriteLine("Selectionner le concert à reserver");
                var response = await bookingService.GetConcertsInformationToReserveAPlaceAsync();
                foreach (var concert in response)
                {
                    Console.WriteLine($"{concert.Id}: {concert.ConcertHall} - {concert.Tickets.Count} places restantes");
                }
                
                var idToReserve = Console.ReadLine();
                var idToReserveInt = Int32.Parse(idToReserve ?? throw new InvalidOperationException());
                var concertToReserve = await bookingService.GetConcertsInformationToReserveById(idToReserveInt);
                try
                {
                    var ticketReserved = await bookingService.ReserveAPlace(concertToReserve);
                    Console.WriteLine(ticketReserved != null
                        ? $"Votre place pour le concert {concertToReserve.ConcertHall} est reservée"
                        : $"Votre place pour le concert {concertToReserve.ConcertHall} n'est pas reservé");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
        }
    }


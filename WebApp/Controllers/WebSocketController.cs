using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamJsonRpc;
using System.Threading.Tasks;
using WebApp.Services;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        private readonly IConcertRepository _concertRepository;
        private readonly IMailingService _mailingService;
        private readonly IPaymentService _paymentService;

        public WebSocketController(IConcertRepository concertRepository, IMailingService mailingService, IPaymentService paymentService)
        {
            _concertRepository = concertRepository;
            _mailingService = mailingService;
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task GetAsync()
        {
            if(HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var messageHandler = new WebSocketMessageHandler(webSocket);
                var jsonRpc = new JsonRpc(messageHandler, new BookingService(_concertRepository, _mailingService, _paymentService));
                
                jsonRpc.StartListening();


                Task.Run(async () =>
                {
                    var bookingNotifications = jsonRpc.Attach<IBookingNotifications>();
                    
                    while (true)
                    {
                        await Task.Delay(5000);
                        bookingNotifications.NotifyClient($"");
                    }
                });

                await jsonRpc.Completion;
            }
        }
    }
}

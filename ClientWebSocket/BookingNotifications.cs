using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWS;

    public class BookingNotifications : IBookingNotifications
    {
        public void NotifyClient(string message)
        {
            Console.WriteLine(message);
        }
    }


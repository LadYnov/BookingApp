using System.Collections.Generic;

namespace WebApp
{
    public class Concert
    {
        public int Id { get; set; }
        public string ConcertHall { get; set; }
        
        public List<Ticket> Tickets { get; set; }
        public string Artist { get; set; }
    }
}

namespace WebApp;

public class ConcertDto
{
    public int Id { get; set; }
    public string ConcertHall { get; set; }
    public string Artist { get; set; }
    public Ticket Ticket {get; set; }
}
namespace APBD_DODATKOWE.Models;

public class Speaker
{
    public int IdSpeaker { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Bio { get; set; }
    
    public ICollection<EventSpeaker> EventSpeakers { get; set; } = new List<EventSpeaker>();
}
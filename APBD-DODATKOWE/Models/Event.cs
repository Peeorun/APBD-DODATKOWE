namespace APBD_DODATKOWE.Models;

public class Event
{
    public int IdEvent { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; } = 0;
    
    public ICollection<EventSpeaker> EventSpeakers { get; set; } = new List<EventSpeaker>();
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
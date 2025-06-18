namespace APBD_DODATKOWE.Models;

public class EventSpeaker
{
    public int IdSpeaker { get; set; }
    public int IdEvent { get; set; }
    public string PresentationTitle { get; set; } = null!;
    
    public Speaker Speaker { get; set; } = null!;
    public Event Event { get; set; } = null!;
}
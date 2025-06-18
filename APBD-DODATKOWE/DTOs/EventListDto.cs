namespace APBD_DODATKOWE.DTOs;

public class EventListDto
{
    public int IdEvent { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int MaxParticipants { get; set; }
    public int CurrentParticipants { get; set; }
    public int AvailableSpots { get; set; }
    public List<string> SpeakerNames { get; set; } = new();
}
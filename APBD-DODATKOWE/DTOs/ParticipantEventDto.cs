namespace APBD_DODATKOWE.DTOs;

public class ParticipantEventDto
{
    public int IdEvent { get; set; }
    public string Title { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<string> SpeakerNames { get; set; } = new();
}
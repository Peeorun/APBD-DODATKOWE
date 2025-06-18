namespace APBD_DODATKOWE.Models;

public class Registration
{
    public int IdEvent { get; set; }
    public int IdParticipant { get; set; }
    public string Status { get; set; } = null!;
    public DateTime RegistrationDate { get; set; }
    
    public Event Event { get; set; } = null!;
    public Participant Participant { get; set; } = null!;
}
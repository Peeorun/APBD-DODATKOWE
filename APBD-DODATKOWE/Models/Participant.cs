namespace APBD_DODATKOWE.Models;

public class Participant
{
    public int IdParticipant { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
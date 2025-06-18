namespace APBD_DODATKOWE.DTOs;

public class ParticipantReportDto
{
    public int IdParticipant { get; set; }
    public string ParticipantName { get; set; } = null!;
    public List<ParticipantEventDto> Events { get; set; } = new();
}
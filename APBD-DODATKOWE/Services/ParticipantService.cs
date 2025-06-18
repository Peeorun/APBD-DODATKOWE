using APBD_DODATKOWE.Data;
using APBD_DODATKOWE.DTOs;
using Microsoft.EntityFrameworkCore;

namespace APBD_DODATKOWE.Services;

public class ParticipantService : IParticipantService
{
    private readonly AppDbContext _context;

    public ParticipantService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message, ParticipantReportDto? Report)> GetParticipantReportAsync(int participantId)
    {
        var participant = await _context.Participants.FindAsync(participantId);
        if (participant == null)
        {
            return (false, "Uczestnik nie został znaleziony", null);
        }

        var events = await _context.Registrations
            .Where(r => r.IdParticipant == participantId)
            .Include(r => r.Event)
            .ThenInclude(e => e.EventSpeakers)
            .ThenInclude(es => es.Speaker)
            .Select(r => new ParticipantEventDto
            {
                IdEvent = r.Event.IdEvent,
                Title = r.Event.Title,
                StartDate = r.Event.StartDate,
                EndDate = r.Event.EndDate,
                SpeakerNames = r.Event.EventSpeakers.Select(es => $"{es.Speaker.FirstName} {es.Speaker.LastName}").ToList()
            })
            .ToListAsync();

        var report = new ParticipantReportDto
        {
            IdParticipant = participantId,
            ParticipantName = $"{participant.FirstName} {participant.LastName}",
            Events = events
        };

        return (true, "Raport wygenerowany pomyślnie", report);
    }
}
using APBD_DODATKOWE.DTOs;
using APBD_DODATKOWE.Models;

namespace APBD_DODATKOWE.Services;

public interface IEventService
{
    Task<(bool Success, string Message, Event? Event)> CreateEventAsync(CreateEventDto dto);
    Task<Event?> GetEventByIdAsync(int id);
    Task<IEnumerable<EventListDto>> GetUpcomingEventsAsync();
    Task<(bool Success, string Message)> AssignSpeakerAsync(int eventId, AssignSpeakerDto dto);
    Task<(bool Success, string Message)> RegisterParticipantAsync(int eventId, RegisterParticipantDto dto);
    Task<(bool Success, string Message)> CancelRegistrationAsync(int eventId, int participantId);
}
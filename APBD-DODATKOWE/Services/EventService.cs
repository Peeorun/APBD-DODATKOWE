using APBD_DODATKOWE.Data;
using APBD_DODATKOWE.DTOs;
using APBD_DODATKOWE.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_DODATKOWE.Services;

public class EventService : IEventService
    {
        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Message, Event? Event)> CreateEventAsync(CreateEventDto dto)
        {
            if (dto.StartDate <= DateTime.Now)
            {
                return (false, "Data wydarzenia nie może być przeszła", null);
            }

            if (dto.EndDate <= dto.StartDate)
            {
                return (false, "Data zakończenia musi być późniejsza niż data rozpoczęcia", null);
            }

            var eventEntity = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                MaxParticipants = dto.MaxParticipants
            };

            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();

            return (true, "Wydarzenie zostało utworzone pomyślnie", eventEntity);
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<IEnumerable<EventListDto>> GetUpcomingEventsAsync()
        {
            return await _context.Events
                .Where(e => e.StartDate > DateTime.Now)
                .Include(e => e.EventSpeakers)
                    .ThenInclude(es => es.Speaker)
                .Select(e => new EventListDto
                {
                    IdEvent = e.IdEvent,
                    Title = e.Title,
                    Description = e.Description,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    MaxParticipants = e.MaxParticipants,
                    CurrentParticipants = e.CurrentParticipants,
                    AvailableSpots = e.MaxParticipants - e.CurrentParticipants,
                    SpeakerNames = e.EventSpeakers.Select(es => $"{es.Speaker.FirstName} {es.Speaker.LastName}").ToList()
                })
                .ToListAsync();
        }

        public async Task<(bool Success, string Message)> AssignSpeakerAsync(int eventId, AssignSpeakerDto dto)
        {
            var eventEntity = await _context.Events.FindAsync(eventId);
            if (eventEntity == null)
            {
                return (false, "Wydarzenie nie zostało znalezione");
            }

            var speaker = await _context.Speakers.FindAsync(dto.IdSpeaker);
            if (speaker == null)
            {
                return (false, "Prelegent nie został znaleziony");
            }
            
            var conflictingEvents = await _context.EventSpeakers
                .Include(es => es.Event)
                .Where(es => es.IdSpeaker == dto.IdSpeaker &&
                           ((es.Event.StartDate <= eventEntity.EndDate && es.Event.EndDate >= eventEntity.StartDate)))
                .AnyAsync();

            if (conflictingEvents)
            {
                return (false, "Prelegent ma konflikt czasowy z innym wydarzeniem");
            }

            var eventSpeaker = new EventSpeaker
            {
                IdEvent = eventId,
                IdSpeaker = dto.IdSpeaker,
                PresentationTitle = dto.PresentationTitle
            };

            _context.EventSpeakers.Add(eventSpeaker);
            await _context.SaveChangesAsync();

            return (true, "Prelegent został przypisany do wydarzenia");
        }

        public async Task<(bool Success, string Message)> RegisterParticipantAsync(int eventId, RegisterParticipantDto dto)
        {
            var eventEntity = await _context.Events.FindAsync(eventId);
            if (eventEntity == null)
            {
                return (false, "Wydarzenie nie zostało znalezione");
            }

            if (eventEntity.CurrentParticipants >= eventEntity.MaxParticipants)
            {
                return (false, "Brak wolnych miejsc na wydarzeniu");
            }

            var participant = await _context.Participants.FindAsync(dto.IdParticipant);
            if (participant == null)
            {
                return (false, "Uczestnik nie został znaleziony");
            }

            var existingRegistration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.IdEvent == eventId && r.IdParticipant == dto.IdParticipant);

            if (existingRegistration != null)
            {
                return (false, "Uczestnik jest już zarejestrowany na to wydarzenie");
            }

            var registration = new Registration
            {
                IdEvent = eventId,
                IdParticipant = dto.IdParticipant,
                Status = "Confirmed",
                RegistrationDate = DateTime.Now
            };

            _context.Registrations.Add(registration);
            eventEntity.CurrentParticipants++;
            await _context.SaveChangesAsync();

            return (true, "Rejestracja zakończona pomyślnie");
        }

        public async Task<(bool Success, string Message)> CancelRegistrationAsync(int eventId, int participantId)
        {
            var eventEntity = await _context.Events.FindAsync(eventId);
            if (eventEntity == null)
            {
                return (false, "Wydarzenie nie zostało znalezione");
            }

            var hoursUntilEvent = (eventEntity.StartDate - DateTime.Now).TotalHours;
            if (hoursUntilEvent < 24)
            {
                return (false, "Nie można anulować rejestracji na mniej niż 24 godziny przed wydarzeniem");
            }

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(r => r.IdEvent == eventId && r.IdParticipant == participantId);

            if (registration == null)
            {
                return (false, "Rejestracja nie została znaleziona");
            }

            _context.Registrations.Remove(registration);
            eventEntity.CurrentParticipants--;
            await _context.SaveChangesAsync();

            return (true, "Rejestracja została anulowana");
        }
    }
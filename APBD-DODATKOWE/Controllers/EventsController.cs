using APBD_DODATKOWE.DTOs;
using APBD_DODATKOWE.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_DODATKOWE.Controllers;


[ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventDto dto)
        {
            var result = await _eventService.CreateEventAsync(dto);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetEvent), new { id = result.Event!.IdEvent }, result.Event);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventEntity = await _eventService.GetEventByIdAsync(id);
            
            if (eventEntity == null)
            {
                return NotFound();
            }

            return Ok(eventEntity);
        }

        [HttpGet]
        public async Task<IActionResult> GetUpcomingEvents()
        {
            var events = await _eventService.GetUpcomingEventsAsync();
            return Ok(events);
        }

        [HttpPost("{eventId}/speakers")]
        public async Task<IActionResult> AssignSpeaker(int eventId, AssignSpeakerDto dto)
        {
            var result = await _eventService.AssignSpeakerAsync(eventId, dto);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpPost("{eventId}/register")]
        public async Task<IActionResult> RegisterParticipant(int eventId, RegisterParticipantDto dto)
        {
            var result = await _eventService.RegisterParticipantAsync(eventId, dto);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpDelete("{eventId}/register/{participantId}")]
        public async Task<IActionResult> CancelRegistration(int eventId, int participantId)
        {
            var result = await _eventService.CancelRegistrationAsync(eventId, participantId);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerStoreController : ControllerBase
    {
        private readonly IEventService _eventService;

        public PartnerStoreController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/PartnerStore/GetAllEvents
        [HttpGet("[action]")]
        public List<Event> GetAllEvents()
        {
            return _eventService.GetAll();
        }

        // GET api/PartnerStore/GetDetailsForEvent/{id}
        [HttpGet("[action]/{id}")]
        public Event GetDetailsForEvent(Guid id)
        {
            return _eventService.GetById(id);
        }
    }
}

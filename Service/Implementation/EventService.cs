using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public void Create(Event e)
        {
            e.Id = Guid.NewGuid();
            _eventRepository.Insert(e);
        }

        public void DeleteById(Guid id)
        {

            _eventRepository.Delete(_eventRepository.Get(id));
        }

        public List<Event> GetAll()
        {
            return _eventRepository.GetAll().ToList();
        }

        public Event GetById(Guid? id)
        {
            return _eventRepository.Get(id);
        }

        public void Update(Event e)
        {
            _eventRepository.Update(e);
        }
    }
}

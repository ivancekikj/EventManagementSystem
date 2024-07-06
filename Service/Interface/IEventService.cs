using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEventService
    {
        List<Event> GetAll();
        Event GetById(Guid? id);
        void Create(Event e);
        void Update(Event e);
        void DeleteById(Guid id);
        void ImportEvents(string fileName);
    }
}

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Schedule> GetAllWithEvent()
        {
            return entities.Include(s => s.Event);
        }

        public Schedule GetWithEvent(Guid? id)
        {
            return entities.Include(s => s.Event)
                .SingleOrDefault(s => s.Id == id);
        }
    }
}

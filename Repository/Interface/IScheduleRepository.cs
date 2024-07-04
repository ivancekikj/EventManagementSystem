using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        IEnumerable<Schedule> GetAllWithEvent();
        Schedule GetWithEvent(Guid? id);
    }
}

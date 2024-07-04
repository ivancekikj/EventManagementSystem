using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IScheduleService
    {
        List<Schedule> GetAll();
        Schedule GetById(Guid? id);
        void Create(Schedule schedule);
        void Update(Schedule schedule);
        void DeleteById(Guid id);
    }
}

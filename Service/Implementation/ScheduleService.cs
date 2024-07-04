using Domain.Models;
using Repository.Implementation;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public void Create(Schedule schedule)
        {
            schedule.Id = Guid.NewGuid();
            _scheduleRepository.Insert(schedule);
        }

        public void DeleteById(Guid id)
        {
            _scheduleRepository.Delete(_scheduleRepository.Get(id));
        }

        public List<Schedule> GetAll()
        {
            return _scheduleRepository.GetAllWithEvent().ToList();
        }

        public Schedule GetById(Guid? id)
        {
            return _scheduleRepository.GetWithEvent(id);
        }

        public void Update(Schedule schedule)
        {
            _scheduleRepository.Update(schedule);
        }
    }
}

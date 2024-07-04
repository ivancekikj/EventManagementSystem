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
    public class TicketForPurchaseService : ITicketForPurchaseService
    {
        private readonly ITicketForPurchaseRepository _ticketRepository;

        public TicketForPurchaseService(ITicketForPurchaseRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public void Create(TicketForPurchase ticket)
        {
            ticket.Id = Guid.NewGuid();
            _ticketRepository.Insert(ticket);
        }

        public void DeleteById(Guid id)
        {
            _ticketRepository.Delete(_ticketRepository.Get(id));
        }

        public List<TicketForPurchase> GetAll()
        {
            return _ticketRepository.GetAllWithSchedule().ToList();
        }

        public TicketForPurchase GetById(Guid? id)
        {
            return _ticketRepository.GetWithSchedule(id);
        }

        public void Update(TicketForPurchase ticket)
        {
            _ticketRepository.Update(ticket);
        }
    }
}

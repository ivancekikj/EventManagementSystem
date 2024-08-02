using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Models;
using ExcelDataReader;
using Microsoft.EntityFrameworkCore.Metadata;
using Org.BouncyCastle.Asn1.Crmf;
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

        public void ImportEvents(string fileName)
        {
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        Create(new()
                        {
                            Name = reader.GetValue(0).ToString(),
                            Location = reader.GetValue(1).ToString(),
                            Description = reader.GetValue(2).ToString()
                        });
                    }
                }
            }
        }

        public void Update(Event e)
        {
            _eventRepository.Update(e);
        }
    }
}

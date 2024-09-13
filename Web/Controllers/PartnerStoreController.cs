using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Web.Controllers
{
    public class PartnerStoreController : Controller
    {
        public IActionResult Events()
        {
            HttpClient client = new HttpClient();
            string URL = "https://emsweb20240826182329.azurewebsites.net/api/Events/GetAllEvents";
            HttpResponseMessage response = client.GetAsync(URL).Result;
            List<PartnerStoreEvent> partnerEvents = response.Content.ReadAsAsync<List<PartnerStoreEvent>>().Result;
            return View(partnerEvents);
        }

        public IActionResult EventDetails(Guid id)
        {
            HttpClient client = new HttpClient();
            string URL = $"https://emsweb20240826182329.azurewebsites.net/api/Events/{id}";
            HttpResponseMessage response = client.GetAsync(URL).Result;
            PartnerStoreEvent partnerEvent = response.Content.ReadAsAsync<PartnerStoreEvent>().Result;
            return View(partnerEvent);
        }
    }
}

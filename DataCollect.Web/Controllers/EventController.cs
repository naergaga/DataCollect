using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataCollect.Service.Service;

namespace DataCollect.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly EventService eventService;

        public EventController(EventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult Publish(int id)
        {
            eventService.Publish(id);
            return RedirectToPage("/Event/Index");
        }

        //public IActionResult FillIn()
        //{

        //}
    }
}
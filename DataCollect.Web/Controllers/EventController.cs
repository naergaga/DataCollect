using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataCollect.Service.Service;
using DataCollect.Web.Data.ViewModels.Event;
using Microsoft.AspNetCore.Http;
using DataCollect.Web.Services.Action;
using DataCollect.Model;
using Microsoft.AspNetCore.Http.Internal;

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

        [HttpGet("/e/{eventName}")]
        public IActionResult FillIn(string eventName,[FromServices]EventService eventService)
        {
            CollectEvent e1 = eventService.Get(eventName);
            return View("/Pages/Event/AddData.cshtml", e1);
        }

        [HttpPost("/e/{eventName}/add/{id}")]
        public IActionResult DoFillIn(int id,string eventName, IFormFile excelfile,
            [FromServices]ImportAction action)
        {
            action.Import(excelfile.OpenReadStream(),id);
            return new RedirectToActionResult("FillIn", "Event", new { eventName = eventName });
        }
    }
}
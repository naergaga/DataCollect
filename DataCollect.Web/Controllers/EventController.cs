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
using Microsoft.AspNetCore.Identity;
using DataCollect.Web.Data;
using DataCollect.Web.Services.Service;
using Microsoft.AspNetCore.Authorization;
using DataCollect.Web.Pages.Event;

namespace DataCollect.Web.Controllers
{
    public class EventController : Controller
    {

        public EventController()
        {
        }


        public IActionResult Item(int id, [FromServices]EventService eventService)
        {
            var model = new ItemModel { CollectEvent = eventService.Get(id) };
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Publish(int id,
            [FromServices]EventService eventService)
        {
            eventService.Publish(id);
            return RedirectToPage("/Event/Index");
        }
        
        [Authorize(Roles="admin")]
        public IActionResult CancelPublish(int id,
            [FromServices]EventService eventService)
        {
            eventService.CanelPublish(id);
            return RedirectToPage("/Event/Index");
        }

        [AllowAnonymous]
        [HttpGet("/e/{eventName}")]
        public IActionResult FillIn(string eventName,
            [FromServices]UserManager<ApplicationUser> userManager,
            [FromServices]EventService eventService)
        {
            if (!eventService.EventPublished(eventName)) { return NotFound(); }
            CollectEvent e1 = eventService.Get(eventName, userManager.GetUserId(User));
            return View("AddData", e1);
        }

        [HttpPost("/e/{eventName}/add/{id}")]
        public IActionResult DoFillIn(int id,string eventName, IFormFile excelfile,
            [FromServices]UserManager<ApplicationUser> userManager,
            [FromServices]ImportAction action)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return NotFound();
            action.Import(excelfile.OpenReadStream(),id, userId);
            return new RedirectToActionResult("FillIn", "Event", new {  eventName });
        }

        [HttpGet("/e/{eventName}/removeRow/{id}")]
        public IActionResult RemoveRow(int id, string eventName, IFormFile excelfile,
            [FromServices]ApplicationDbContext context,
            [FromServices]RowService rowService,
            [FromServices]UserManager<ApplicationUser> userManager)
        {
            var userId = userManager.GetUserId(User);
            var row = context.Row.SingleOrDefault(t => t.Id == id && t.UserId == userId);
            if (row == null) return NotFound();
            rowService.Remove(row);
            return new RedirectToActionResult("FillIn", "Event", new { eventName });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataCollect.Service.Service;
using DataCollect.Model;

namespace DataCollect.Web.Pages.Event
{
    public class IndexModel : PageModel
    {
        private EventService eventService;

        public List<CollectEvent> EventList { get; set; }

        public IndexModel(EventService eventService)
        {
            this.eventService = eventService;
        }

        public void OnGet()
        {
            this.EventList = eventService.GetList();
        }
    }
}
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
    public class ItemModel : PageModel
    {
        private EventService eventService;

        public CollectEvent CollectEvent { get; set; }

        public ItemModel(EventService eventService)
        {
            this.eventService = eventService;
        }

        public void OnGet(int id)
        {
            this.CollectEvent=eventService.Get(id);
        }
    }
}
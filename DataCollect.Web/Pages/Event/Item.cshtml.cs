using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataCollect.Service.Service;
using DataCollect.Model;
using Microsoft.AspNetCore.Authorization;
using DataCollect.Web.Utities.Option;

namespace DataCollect.Web.Pages.Event
{
    public class ItemModel 
    {
        public CollectEvent CollectEvent { get; set; }
        public PageOption PageOption { get; set; }
    }
}
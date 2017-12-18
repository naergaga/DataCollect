using DataCollect.Model;
using DataCollect.Web.Utities.Option;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Utities.ViewComponents
{
    public class PageOptionModel{
        public PageOption Option { get; set; }
        public IDictionary<string,string> Routes { get; set; }
    }

    public class PageViewComponent : ViewComponent
    {
        public PageViewComponent()
        {

        }

        public IViewComponentResult Invoke(PageOptionModel option)
        {
            return View(option);
        }
    }
}

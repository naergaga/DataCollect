using DataCollect.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Utities.ViewComponents
{
    public class BookModel
    {
        public Book Book { get; set; }
        public bool Edit { get; set; } = false;
        public bool CanExport { get; set; } = false;
    }


    public class BookViewComponent : ViewComponent
    {
        public BookViewComponent()
        {

        }

        public IViewComponentResult Invoke(BookModel model)
        {
            return View(model);
        }
    }
}

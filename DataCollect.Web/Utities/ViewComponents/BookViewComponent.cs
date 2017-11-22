using DataCollect.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Utities.ViewComponents
{
    public class BookViewComponent:ViewComponent
    {
        public BookViewComponent()
        {

        }

        public IViewComponentResult Invoke(Book book)
        {
            return View(book);
        }
    }
}

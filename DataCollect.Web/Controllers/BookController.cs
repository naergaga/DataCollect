using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCollect.Service.Service;
using DataCollect.Web.Services.Action;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataCollect.Web.Controllers
{
    public class BookController : Controller
    {
        [Authorize(Roles ="admin")]
        public IActionResult Export(int id,
        [FromServices]BookService bookService,
        [FromServices]ExportAction action)
        {
            var book = bookService.GetWithData(id);

            var bytes = action.Export(book);
            return File(bytes, "application/octet-stream",book.Name+".xlsx");
        }
    }
}
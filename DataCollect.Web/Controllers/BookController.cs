using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCollect.Model;
using DataCollect.Service.Service;
using DataCollect.Web.Data;
using DataCollect.Web.Services.Action;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        [HttpPost("/e/{eventName}/addRow/{sheetId}")]
        public IActionResult AddRow(int sheetId,
            string eventName,
            List<string> rowData,
            [FromServices]SheetService service,
            [FromServices]ApplicationDbContext context,
            [FromServices]UserManager<ApplicationUser> userManager
        )
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return NotFound();
            var cols = service.GetColumns(sheetId);
            var row = new Row { SheetId = sheetId, UserId = userId };
            row.Data = cols.Select((col,index)=>
            {
                return new ColumnData { ColumnId = col.Id, Value = rowData[index] };
            }).ToList();
            context.Add(row);
            context.SaveChanges();
            return RedirectToAction("FillIn", "Event", new { eventName });
        }
    }
}
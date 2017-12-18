using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCollect.Web.Data;
using DataCollect.Web.Data.Json;
using DataCollect.Web.Services.Action;
using DataCollect.Web.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DataCollect.Web.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/EventAction")]
    public class EventActionController : Controller
    {
        [HttpPost]
        public IActionResult DoFillIn(int id, IFormFile excelfile,
            [FromServices]UserManager<ApplicationUser> userManager,
            [FromServices]ImportAction action)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return NotFound();
            try
            {
                action.Import(excelfile.OpenReadStream(), id, userId);
            }
            catch
            {
                return Json(new DoActionResult
                {
                    Success = false,
                    Message = "添加失败，请检查excel内容。"
                });
            }
            return Json(new DoActionResult
            {
                Success = true,
                Message = ""
            });
        }

        [HttpPost("RemoveRow")]
        public IActionResult RemoveRow(int id,
            [FromServices]ApplicationDbContext context,
            [FromServices]RowService rowService,
            [FromServices]UserManager<ApplicationUser> userManager)
        {
            var userId = userManager.GetUserId(User);
            var row = context.Row.SingleOrDefault(t => t.Id == id && t.UserId == userId);
            if (row == null) return NotFound();
            try
            {
                rowService.Remove(row);
                return Json(new DoActionResult
                {
                    Success = true,
                    Message = ""
                });
            }
            catch
            {
                return Json(new DoActionResult
                {
                    Success = false,
                    Message = "删除行失败"
                });
            }

        }
    }
}
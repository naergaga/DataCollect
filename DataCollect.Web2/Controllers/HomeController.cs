using DataCollect.Service.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataCollect.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Preview()
        {
            if (Request.Files.Count < 0) return Json(null);
            var file = Request.Files[0];
            var book = BookProvider.Get(file.InputStream,file.FileName);
            return Json(book);
        }

        public ActionResult Import()
        {
            if (Request.Files.Count < 0) return RedirectToAction("Index");
            var file = Request.Files[0];
            var book = BookProvider.Get(file.InputStream, file.FileName);

            return RedirectToAction("Index");
        }
    }
}
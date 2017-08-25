using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebDash.Controllers
{
    public class ReadModelController : Controller
    {
        private const string Title = "Read Model";

        // GET: ReadModel
        public ActionResult Index()
        {
            ViewBag.Title = Title;

            return View();
        }
    }
}
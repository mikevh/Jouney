using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Journey.Web.Models;

namespace Journey.Web.Controllers
{
    public class MeetingsController : Controller
    {
        public ActionResult Index() {
            return View();
        }
    }
}

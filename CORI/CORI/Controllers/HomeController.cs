using CORI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CORI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        
        {
           // DataLayer.DataLayer test = new DataLayer.DataLayer();
          //  test.testing(6);
            return View();
        }
        [HttpGet]
        public ActionResult App()
        {
            return View();
        }
        [HttpPost]
        public ActionResult App(Stranica test)
        {
            
            JsonResult t = new JsonResult();
            t.ContentType = "string";
            t.Data = "Test";
            return t;
        }
    }
}

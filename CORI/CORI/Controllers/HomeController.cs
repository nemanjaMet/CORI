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
        public ActionResult App()
        {
            return View();
        }

    }
}

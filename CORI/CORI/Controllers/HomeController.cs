using CORI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

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
            HtmlListModel hlm = new HtmlListModel();
            return View(hlm);
        }
        [HttpPost]
        public ActionResult App(Stranica test)
        {
            
            JsonResult t = new JsonResult();
            t.ContentType = "string";
            t.Data = "Test";
            return t;
           // DataLayer.DataLayer test2 = new DataLayer.DataLayer();
            
        }


        // userName treba da bude isto i u JQUERY.AJAX.POST
        [HttpPost]
        public ActionResult MyHtml(string userName)
        {
            User testUser = new User();
            testUser.userName = "omega";
            Stranica s1 = new Stranica();
            Stranica s2 = new Stranica();
            Stranica s3 = new Stranica();
            s1.ime = "stranica 1"; s1.html = "<div>html broj 1</div>";
            s2.ime = "stranica 2"; s2.html = "<div>html broj 2</div>";
            s3.ime = "stranica 3"; s3.html = "<div>html broj 3</div>";
            List<Stranica> listaStranica = new List<Stranica>();
            listaStranica.Add(s1);
            listaStranica.Add(s2);
            listaStranica.Add(s3);
            testUser.pageList = listaStranica;
            JsonResult t = new JsonResult();
            t.ContentType = "string";
            t.Data = testUser;
            return t;
        }

        [HttpPost]
        public ActionResult OtherPeopleHtml(string otherPeople)
        {
            Stranica s1 = new Stranica();
            Stranica s2 = new Stranica();
            Stranica s3 = new Stranica();
            s1.ime = "stranica 1"; s1.html = "html broj 1";
            s2.ime = "stranica 2"; s2.html = "html broj 2";
            s3.ime = "stranica 3"; s3.html = "html broj 3";
            List<Stranica> listaStranica = new List<Stranica>();
            listaStranica.Add(s1);
            listaStranica.Add(s2);
            listaStranica.Add(s3);
            JsonResult t = new JsonResult();
            t.ContentType = "string";
            t.Data = listaStranica;
            return t;
        }

    }
}

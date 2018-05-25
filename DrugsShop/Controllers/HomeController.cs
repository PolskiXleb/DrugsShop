using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrugsShop.Models;

namespace DrugsShop.Controllers
{
    public class HomeController : Controller
    {
        DrugsShopEntities db = new DrugsShopEntities();

        public ActionResult Index()
        {

            IEnumerable<Order> q =
                from o in db.Orders
                where o.ClientId == 2
                select o;
            return View(q);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
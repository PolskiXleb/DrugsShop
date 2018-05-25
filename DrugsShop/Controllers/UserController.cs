using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrugsShop.Models;
using System.Data;

namespace DrugsShop.Controllers
{
    public class UserController : Controller
    {
        DrugsShopEntities db = new DrugsShopEntities();
        // GET: User

        public ActionResult Register()
        {
            
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(string fName, string mName, string lName, string address, string city, string phoneNumber)
        //{
        //    Client client = new Client();

        //    client.FName = fName;
        //    client.MName = mName;
        //    client.LName = lName;
        //    client.Address = address;
        //    client.City = city;
        //    client.PhoneNumber = phoneNumber;

        //    db.Clients.Add(client);
        //    db.SaveChanges();

        //    return View("SucessRegistration");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "FName, MName, LName, Address, City, PhoneNumber")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Clients.Add(client);
                    db.SaveChanges();
                    client = db.Clients.Where(s => s.PhoneNumber == client.PhoneNumber).FirstOrDefault();
                    if (CurrentUser.PartialRegistration)
                    {
                       // return View("","",)
                    }
                    else
                    {
                        CurrentUser.Status = Status.loggedIn;
                        CurrentUser.Id = client.Id;
                        
                        return RedirectToAction("SucessRegistration", new { fName = client.FName });
                    }

                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "unable to save changes, try again");
            }
            return View(client);  
        }

       
        public ActionResult Logout(bool confirmation)
        {
            if (confirmation)
            {
                CurrentUser.Id = -1;
                CurrentUser.Status = Status.freeUser;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View();
            }
        }


        public ActionResult SucessRegistration(string fName)
        {
            ViewBag.FName = fName;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string phoneNumber)
        {
            ViewBag.PhoneNumber = phoneNumber;
            if (phoneNumber == null) phoneNumber = "0";
            Client client = db.Clients.Where(s => s.PhoneNumber == phoneNumber).FirstOrDefault();
            if (client != null)
            {
                CurrentUser.Status = Status.loggedIn;
                CurrentUser.Id = client.Id;
                return RedirectToAction("Index", "Product");
            }
            else
            {
                
                    //ViewBag.Message = "Пользователь с введенным номером телефона не найден";
                    return View();
                
            }
        }
    }
}
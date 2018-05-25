using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrugsShop.Models;
using PagedList;

namespace DrugsShop.Controllers
{
    public class ProductController : Controller
    {
        DrugsShopEntities db = new DrugsShopEntities();
        // GET: Product
        public ActionResult Index(int? page, string searchString, string sortParam, bool onlyWithoutRecipe = false)
        {
            
            IEnumerable<Product> products = db.Products;

            if (sortParam == null) sortParam = "name";

            

            products = Filter(products, onlyWithoutRecipe);
            products = Search(products, searchString);
            products = Sort(products, sortParam);
            products = Page(products, searchString, page);

            return View(products);
        }

        public ActionResult Checkout(int orderId)
        {
            if (CurrentUser.Status == Status.loggedIn)
            {
                OrderContent content = db.OrderContents.Where(s => s.OrderId == orderId).First();
                return View(content);
            }
            else
            {
                CurrentUser.PartialRegistration = true;
                return View("~Views/User/Login");
            }
        }

        private IEnumerable<Product> Sort(IEnumerable<Product> products, string sortParam)
        {
            ViewBag.CurrentSort = sortParam;


            switch (sortParam)
            {
                case "name":
                    ViewBag.NameSortParam = "name_desc";
                    products = products.OrderBy(n => n.Name);
                    break;

                case "name_desc":
                    ViewBag.NameSortParam = "name";
                    products = products.OrderByDescending(n => n.Name);
                    break;

                case "cost":
                    ViewBag.CostSortParam = "cost_desc";
                    products = products.OrderBy(n => n.Cost);
                    break;

                case "cost_desc":
                    ViewBag.CostSortParam = "cost";
                    products = products.OrderByDescending(n => n.Cost);
                    break;
            }

            return products;
        }
        
        private IEnumerable<Product> Search(IEnumerable<Product> products, string searchString)
        {
            ViewBag.SearchString = searchString;

            if (searchString != null && searchString != "")
            {
                products = products.Where(n => n.Name.ToLower().Contains(searchString.ToLower()));
            }

            return products;
        }

        private IEnumerable<Product> Page(IEnumerable<Product> products, string searchString, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            if (searchString != null)
            {
                page = 1;
            }

            return products.ToPagedList(pageNumber, pageSize);
        }

        private IEnumerable<Product> Filter(IEnumerable<Product> products, bool onlyWithoutRecipe)
        {
            ViewBag.OnlyWithoutRecipe = onlyWithoutRecipe;
            
            if (onlyWithoutRecipe)
            {
                products = products.Where(p => p.ByRecipe == false);
            }

            return products;
        }
    }
}
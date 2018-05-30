using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrugsShop.Models;

namespace DrugsShop.Controllers
{
    public class CartController : Controller
    {
        DrugsShopEntities db = new DrugsShopEntities();
        // GET: Cart

        [HttpGet]
        public ActionResult GetCart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCart(Position[] position)
        {
            int sum = 0;
            List<CartProduct> cart = new List<CartProduct>();
            for (int i = 0; i < position.Length; i++)
            {
                CartProduct product = new CartProduct();
                int pId = position[i].Id;
                List<Product> prs = db.Products.ToList();
                Product pr = prs.First(s => s.Id == pId );
                product.Amount = position[i].Amount;
                product.ByRecipe = pr.ByRecipe;
                product.Cost = pr.Cost;
                product.Id = pr.Id;
                product.Name = pr.Name;
                product.Sum = pr.Cost * product.Amount;
                sum += product.Sum;

                cart.Add(product);
            }

            ViewBag.Sum = sum;
            return View(cart);
        }

        public ActionResult Checkout(Position[] positions)
        {
            int userId = CurrentUser.Id;
            Order order = new Order();
            int orderId;

            order.ClientId = userId;
            order.OrderCode = GetCode();
            order.Date = DateTime.Now;

            db.Orders.Add(order);
            db.SaveChanges();
            orderId = db.Orders.Last().Id;

            foreach (Position p in positions)
            {
                OrderContent orderC = new OrderContent();
                orderC.Amount = p.Amount;
                orderC.OrderId = orderId;
                orderC.ProductId = p.Id;

                db.OrderContents.Add(orderC);
                db.SaveChanges();
            }

            return View();
        }

        private string GetCode()
        {
            Random r = new Random();
            string code = r.Next(100000, 999999).ToString();

            return code;
        }

    }
}
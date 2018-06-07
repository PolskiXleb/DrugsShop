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

        [HttpPost]
        public PartialViewResult CalculateCart(string ids, string amounts)
        {
            CartFinal cartFinal = new CartFinal();

            cartFinal.Cart = new List<CartProduct>();
            List<Product> prs = db.Products.ToList();
            List<Position> positions = new List<Position>();

            for (int i = 0; i < ids.Count(s => s == ',') + 1; i++)
            {
                positions.Add(new Position { Id = ids.Split(',').Select(n => Convert.ToInt32(n)).ToArray()[i], Amount = amounts.Split(',').Select(n => Convert.ToInt32(n)).ToArray()[i] });

            }


            foreach (Position p in positions)
            {
                CartProduct product = new CartProduct();

                Int32 pId = p.Id;
                Product pr = prs.First(s => s.Id == pId);
                product.Amount = p.Amount;
                product.ByRecipe = pr.ByRecipe;
                product.Cost = pr.Cost;
                product.Id = pr.Id;
                product.Name = pr.Name;
                product.Sum = pr.Cost * product.Amount;

                cartFinal.Cart.Add(product);
            }

            return PartialView(cartFinal);
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
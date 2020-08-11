using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book.Controllers
{
    public class OrderController : Controller
    {
      
            private IOrderRepository repository;
            private Cart cart;
            public OrderController(IOrderRepository repoService, Cart cartService)
            {
                repository = repoService;
                cart = cartService;
            }
        [Authorize]
        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        [Authorize]
        public IActionResult Checkout(Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
        
                    if (ModelState.IsValid) {
            order.Lines = cart.Lines.ToArray();
            repository.SaveOrder(order);
            return RedirectToAction(nameof(Completed));
                } else {
            return View(order);
            }
            }
        [Authorize]
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
}
}
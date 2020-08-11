using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Data;
using Book.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book.Controllers
{
   
    public class CategoryController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            return View(context.Categories.ToList());
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(Category cat)
        {
            
            if (ModelState.IsValid)
            {
                Category cnew = new Category();
                cnew.CategoryName = cat.CategoryName;
                context.Categories.Add(cnew);
                context.SaveChanges();
                return View("Index", context.Categories);

            }
            
            return View("Create", cat);


        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            Category cnew = context.Categories.FirstOrDefault(cat => cat.CategoryID == id);

            return View(cnew);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, Category cat)
        {

            if (ModelState.IsValid)
            {
                Category cold = context.Categories.FirstOrDefault(cat => cat.CategoryID == id);
                cold.CategoryName = cat.CategoryName;
               

                context.SaveChanges();
            }


            return View("Index", context.Categories);
        }
        [Route("Category/Details/{CategoryID}")]
        public IActionResult Details(int CategoryID)
        {
            var cat = context.Categories.FirstOrDefault(repo => repo.CategoryID == CategoryID);
            return View(cat);
        }
        public IActionResult Delete(int id)
        {
            Category cold = context.Categories.FirstOrDefault(cat => cat.CategoryID == id);
            context.Categories.Remove(cold);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
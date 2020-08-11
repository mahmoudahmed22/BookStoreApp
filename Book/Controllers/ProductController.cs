using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Book.Data;
using Book.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Book.ViewModels;

using X.PagedList;
namespace Book.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private readonly IHostingEnvironment _hosting;
        private IProductRepository repository;
        public ProductController(IProductRepository repo, IHostingEnvironment hosting)
        {
            _hosting = hosting;
            repository = repo;
        }
        [HttpPost]
        public IActionResult ListSearch(string search, int? page)
        {
            var pagenumber = page ?? 1;
            ViewBag.Products = repository.Products.ToList().ToPagedList(pagenumber, 2);
            var pro = repository.Products.Where(prd => prd.Name.Contains(search)).ToList().ToPagedList(pagenumber, 2);
            if (pro != null)
            {
                return View("List", pro);
            }
            else
            {
                var pro1 = context.Products.ToPagedList(pagenumber, 2);
                return View("List", pro1);
            }
        }
        public IActionResult List(int id, int? page)
        {
            var pagenumber = page ?? 1;
            ViewBag.Products = repository.Products.ToList().ToPagedList(pagenumber, 2);
            var pro = repository.Products.Where(cat => cat.CategoryID == id).ToPagedList(pagenumber, 2);
            if (pro != null)
            {
                return View("List", pro);
            }
            else
            {
                var pro1 = context.Products.ToPagedList(pagenumber, 2);
                return View("List",pro1);
            }
        }
        public IActionResult Index(int ? page)
        {
            var pagenumber = page ?? 1;
            ViewBag.Products = repository.Products.ToList().ToPagedList(pagenumber,2);
            return View(repository.Products.ToList().ToPagedList(pagenumber, 2));
                
        }
        [Route("Product/Detail/{ProductID}")]
        public IActionResult Detail(int ProductID)
        {
            var repo = repository.Products;
            return View(repo.FirstOrDefault(repo=>repo.ProductID==ProductID));
        }
        public IActionResult Create()
        {
            ViewBag.cat = context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (productViewModel.File != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, @"uploads");
                string fullpath = Path.Combine(uploads, productViewModel.File.FileName);
                productViewModel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
            }


            if (ModelState.IsValid)
            {
                Product pnew = new Product();
                pnew.Name = productViewModel.Name;
                pnew.Description = productViewModel.Description;
                pnew.img = productViewModel.File.FileName;
                pnew.Price = productViewModel.Price;
                pnew.Quantity = productViewModel.Quantity;
                pnew.CategoryID = productViewModel.CategoryID;
                context.Products.Add(pnew);
                context.SaveChanges();
                return View("Index", context.Products);

            }
            ViewBag.cat = context.Categories.ToList();

            return View("Create",productViewModel);


        }
        public IActionResult Edit(int id)
        {
            Product pnew = context.Products.FirstOrDefault(Product => Product.ProductID == id);
            List<Category>cat=context.Categories.ToList();
            ViewBag.CategoryID = cat;
            return View(pnew);
        }
        [HttpPost]
        public IActionResult Edit(int id,ProductViewModel productViewModel)
        {
            if (productViewModel.File != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, @"uploads");
                string fullpath = Path.Combine(uploads, productViewModel.File.FileName);
                productViewModel.File.CopyTo(new FileStream(fullpath, FileMode.Create));
            }


            if (ModelState.IsValid)
            {
                Product pold=context.Products.FirstOrDefault(Product => Product.ProductID == id);
                pold.Name = productViewModel.Name;
                pold.Description = productViewModel.Description;
                if (productViewModel.File != null)
                {
                    pold.img = productViewModel.File.FileName;

                }
                else
                {
                    pold.img = pold.img;

                }
                pold.Quantity = productViewModel.Quantity;

                pold.Price = productViewModel.Price;
                pold.CategoryID = productViewModel.CategoryID;

                context.SaveChanges();
            }


            return View("Index", context.Products);
        }
        public IActionResult Delete(int id)
        {
            Product pselect = context.Products.FirstOrDefault(Product => Product.ProductID == id);
            context.Products.Remove(pselect);
            context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

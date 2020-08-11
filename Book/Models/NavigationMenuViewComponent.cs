using Book.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Models
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        private IProductRepository repository;
        ApplicationDbContext context = new ApplicationDbContext();
        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View(context.Categories.ToList());
        }
    }
}

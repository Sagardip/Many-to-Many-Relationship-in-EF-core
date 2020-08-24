using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using one.Models;
using one.ViewModels;

namespace one.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext dbContext;

        public CategoriesController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var category = dbContext.Categories.OrderByDescending(c => c.CategoryId).ToList();
            return View(category);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var category = dbContext.Categories.Include(p => p.PostCategories)
                .ThenInclude(c => c.Post)
                .FirstOrDefault(p => p.CategoryId == id);
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var category = new Category();

            return View(category);

        }

        [HttpPost]
        public IActionResult Create([FromForm] Category category)
        {
            var a = dbContext.Categories.Select(cn => cn.CategoryName.ToUpper().Trim()).ToList();

            if (a.Contains(category.CategoryName.ToUpper().Trim()))
            {
                TempData["categorymessage"] = "Category already exists !!!";
                return View();
            }
            else
            {

                dbContext.Add(category);
                dbContext.SaveChanges();
                TempData["message"] = "Successfully Added";
                return RedirectToAction("Index");
            }

        }
    }
}
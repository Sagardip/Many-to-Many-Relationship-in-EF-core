using System.Runtime.Intrinsics.X86;
using System.Xml.Serialization;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using one.Models;
using one.Services;
using one.ViewModels;

namespace one.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IFileService fileService;

        public IWebHostEnvironment HostingEnvironment => hostingEnvironment;

        public PostsController(AppDbContext dbContext, IWebHostEnvironment environment,
            IFileService fileService)
        {
            this.dbContext = dbContext;
            hostingEnvironment = environment;
            this.fileService = fileService;
        }
        public static string getpath;
        public IActionResult Index()
        {

            var post = dbContext.Posts.Include(p => p.PostCategories)
                .ThenInclude(c => c.Category)
                .OrderByDescending(p => p.PostId);

            return View(post);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            var post = dbContext.Posts.Include(p => p.PostCategories)
                .ThenInclude(c => c.Category).FirstOrDefault(m => m.PostId == id);
            return View(post);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var Post = dbContext.Posts.FirstOrDefaultAsync(c => c.PostId == id);
            return View();

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var post = dbContext.Posts.Find(id);
            dbContext.Posts.Remove(post);
            dbContext.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                var post = dbContext.Posts
                    .Include(p => p.PostCategories)
                        .Where(p => p.PostId == id).FirstOrDefault();
                if (post == null) return View();

                var PostVM = new PostCreateVM()
                {
                    PostId = post.PostId,
                    Title = post.Title,
                    Description = post.Description,
                    EditImagePath = post.DisplayImage,
                    Categories = dbContext.Categories.ToList(),
                    SelectedCategory = post.PostCategories.Select(pc => pc.CategoryId).ToList()

                };
                ViewBag.Categories = new SelectList(dbContext.Categories, "CategoryId", "CategoryName");
                return View(PostVM);
            }
            else
            {
                var post = new PostCreateVM();
                post.Categories = dbContext.Categories.ToList();
                ViewBag.Categories = new SelectList(dbContext.Categories, "CategoryId", "CategoryName");
                return View(post);

            }
        }

        [HttpPost]

        public IActionResult Create([FromForm] PostCreateVM vm, int? id)
        {
            if (id != null)
            {
                var post = dbContext.Posts.Include(p => p.PostCategories)
                    .Where(p => p.PostId == id).FirstOrDefault();

                if (vm.DisplayImage != null)
                {  post.DisplayImage = fileService.Upload(vm.DisplayImage);
                    var fileName = fileService.Upload(vm.DisplayImage);
                    post.DisplayImage = fileName;
                    getpath = fileName;
                }
                post.Title = vm.Title;
                post.Description = vm.Description;
                post.PostCategories = new List<PostCategory>();

                foreach (var CategoryId in vm.SelectedCategory)
                {
                    post.PostCategories.Add(new PostCategory { CategoryId = CategoryId });
                }
                dbContext.SaveChanges();
                TempData["Editmessage"] = "Edited Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                Post post = new Post()
                {

                    PostId = vm.PostId,
                    Description = vm.Description,
                    Title = vm.Title,
                    DisplayImage = vm.DisplayImage.FileName,
                };
                if (ModelState.IsValid)
                {
                    var fileName = fileService.Upload(vm.DisplayImage);
                    post.DisplayImage = fileName;
                    getpath = fileName;

                    dbContext.Add(post);
                    dbContext.SaveChanges();
                }
                foreach (var cat in vm.SelectedCategory)
                {

                    PostCategory postCategory = new PostCategory();
                    postCategory.PostId = post.PostId;
                    postCategory.CategoryId = cat;
                    dbContext.Add(postCategory);
                    dbContext.SaveChanges();
                }
                TempData["message"] = "Successfully Added";
                return RedirectToAction("Index");
            }

        }
    }
}
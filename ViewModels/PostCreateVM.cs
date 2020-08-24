using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using one.Models;
namespace one.ViewModels
{
    public class PostCreateVM
    {

      
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public List<int> SelectedCategory { get; set; }
        public string EditImagePath { get; set; }
        public IFormFile DisplayImage { get; set; }
    }
}
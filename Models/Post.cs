using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
namespace one.Models
{
    public class Post
    {
        public int PostId { get; set; }
        [Required(ErrorMessage = "Name length can't be more than 8.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<PostCategory> PostCategories { get; set; }
        public string DisplayImage { get; set; }
        [Display(Name = "PublishedDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime PublishedDate { get; set; }
        public Post()
        {
            PostCategories = new Collection<PostCategory>();
            PublishedDate = DateTime.Now;
        }
    }
}
using System;
using System.Collections.Generic;
namespace one.Models
{
    public class PostCategory
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }
}
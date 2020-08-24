using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace one.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<PostCategory> PostCategories { get; set; }

        public static implicit operator List<object>(Category v)
        {
            throw new NotImplementedException();
        }

        internal object ToUpper()
        {
            throw new NotImplementedException();
        }
    }
}
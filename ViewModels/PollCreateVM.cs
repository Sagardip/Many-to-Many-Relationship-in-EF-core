using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using one.Models;

namespace one.ViewModels
{
    public class PollCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string Question { get; set; }
        public string Title { get; set; }
        [Required]
        public string Answer { get; set; }
        public virtual ICollection<PollOption> PollOptions { get; set; }
    }
}
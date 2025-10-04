using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Entities
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string Text { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }

        // Foreign keys
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        [ForeignKey("WriterId")]
        public int WriterId { get; set; }

        // Navigation
        public Category Category { get; set; }
        public Writer Writer { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}

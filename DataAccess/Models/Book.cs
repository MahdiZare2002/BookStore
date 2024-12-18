using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Img { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author? Author { get; set; }
        public bool? isActive { get; set; }
        public bool? inHomePage { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public ICollection<BasketItems>? BasketItems { get; set; }
    }
}

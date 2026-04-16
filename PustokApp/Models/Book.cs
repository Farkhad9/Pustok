using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokApp.Models
{
    public class Book : BaseEntity

    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string Code { get; set; }
        public bool InStock { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public string MainImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<BookImage> BookImages { get; set; }
        public List<BookTag> BookTags { get; set; }
    }
}

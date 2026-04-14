namespace PustokApp.Models
{
    public class Book : BaseEntity

    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercent { get; set; }
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

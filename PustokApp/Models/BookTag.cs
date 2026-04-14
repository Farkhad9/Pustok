namespace PustokApp.Models
{
    public class BookTag : BaseEntity
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}

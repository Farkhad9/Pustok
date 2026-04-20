namespace PustokApp.ViewModels
{
    public class BasketItemVm
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string MainImageUrl { get; set; }
    }
}

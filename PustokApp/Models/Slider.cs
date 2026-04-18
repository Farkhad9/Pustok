using PustokApp.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokApp.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ButtonLink { get; set; }
        public string ButtonText { get; set; }
        public bool IsMain { get; set; }
        [NotMapped]
        [FileLength(2)]
        [FileTypes ("image/jpg", "image/png")]
        public IFormFile File { get; set; }

    }

}

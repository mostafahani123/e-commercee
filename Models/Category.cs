using System.ComponentModel.DataAnnotations;

namespace e_commercee.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Decription { get; set; }
        public string Image { get; set; }
    }
}

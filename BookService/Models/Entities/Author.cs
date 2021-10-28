using System.ComponentModel.DataAnnotations;

namespace BookService.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
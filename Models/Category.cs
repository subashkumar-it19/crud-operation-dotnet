using System.ComponentModel.DataAnnotations;

namespace crud.Models{
    public class Category{
        public long Id {get; set;}
        [Required]
        public string? Name {get; set;}
        [Required]
        public string? Description {get; set;}
    }
}
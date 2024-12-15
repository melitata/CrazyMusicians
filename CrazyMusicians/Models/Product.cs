using System.ComponentModel.DataAnnotations;

namespace CrazyMusicians.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Name must be less than 50 characters")]

        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100")]
        public int Age { get; set; }

        [Range(1, 100, ErrorMessage = "Number of albums must be between 1 and 100")]
        public int NumberOfAlbums { get; set; }


    }
}

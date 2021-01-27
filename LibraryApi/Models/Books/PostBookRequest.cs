using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models.Books
{
    public class PostBookRequest : IValidatableObject
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(200)]
        public string Author { get; set; }
        [MaxLength(100)]
        public string Genre { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Title.Trim().ToLower() == "it" && Author.Trim().ToLower() == "king")
            {
                yield return new ValidationResult("We hate that book", 
                    new string[] { nameof(Title), nameof(Author) });
            }
        }
    }

}

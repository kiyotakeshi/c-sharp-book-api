using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApi.Models
{
    public class Author
    {
        [Key] // write explicitly
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "First Name connot be more than 100 characters")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Last Name connot be more than 150 characters")]
        public string LastName { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}

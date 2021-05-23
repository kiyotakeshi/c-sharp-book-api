using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApi.Models
{
    public class Reviewer
    {
        [Key] // write explicitly
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Reviewer Fistname mubst be up to 50 characters in length")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Reviewer Lastname mubst be up to 50 characters in length")]
        public string LastName { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}

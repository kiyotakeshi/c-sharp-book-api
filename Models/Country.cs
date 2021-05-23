using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApi.Models
{
    public class Country
    {
        [Key] // write explicitly
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Country mubst be up to 50 characters in length")]
        public virtual ICollection<Author> Authors { get; set; }
    }
}

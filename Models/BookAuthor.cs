namespace BookApi.Models
{
    public class BookAuthor
    {
        // foreign key
        public int BookId { get; set; }
        public Book Book { get; set; }

        // foreign key
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
